using FineBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbRepository.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="ArgumentException"></exception>
        public BaseSpecification(QueryParameters parameters)
        {
            // Фильтрация
            foreach (var filter in parameters.Filters)
            {
                if (filter.Key.Contains('.'))
                {
                    AddIncludeForPath(filter.Key);
                }
            }

            Criteria = BuildCriteria(parameters);

            // Сортировка
            // Автоматическое включение для сортировки
            if (!string.IsNullOrEmpty(parameters.SortBy) && parameters.SortBy.Contains('.'))
            {
                AddIncludeForPath(parameters.SortBy);
            }

            ApplyPaging((parameters.PageNumber - 1) * parameters.PageSize, parameters.PageSize);

            if (!string.IsNullOrEmpty(parameters.SortBy))
            {
                if (!PropertyExists(typeof(T), parameters.SortBy))
                    throw new ArgumentException($"Invalid sort property: {parameters.SortBy}");

                var orderExpression = CreateOrderExpression(parameters.SortBy);

                if (!string.IsNullOrEmpty(parameters.SortBy))
                {
                    if (parameters.SortDescending)
                    {
                        ApplyOrderByDescending(orderExpression);
                    }
                    else
                    {
                        ApplyOrderBy(orderExpression);
                    }
                }
            }

            // Пагинация
            if (parameters.PageNumber > 0 && parameters.PageSize > 0)
            {
                ApplyPaging((parameters.PageNumber - 1) * parameters.PageSize, parameters.PageSize);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeExpression"></param>
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            // Преобразуем выражение в строковый путь для IncludeStrings
            var path = GetIncludePath(includeExpression);
            if (!string.IsNullOrEmpty(path))
            {
                IncludeStrings.Add(path);
            }

            // Также добавляем оригинальное выражение
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string GetIncludePath(Expression<Func<T, object>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return GetMemberPath(memberExpression);
            }
            else if (expression.Body is UnaryExpression unaryExpression &&
                     unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                return GetMemberPath(unaryMemberExpression);
            }

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberExpression"></param>
        /// <returns></returns>
        private string GetMemberPath(MemberExpression memberExpression)
        {
            var path = new List<string>();
            while (memberExpression != null)
            {
                path.Add(memberExpression.Member.Name);
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            path.Reverse();
            return string.Join(".", path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includePath"></param>
        /// <exception cref="ArgumentException"></exception>
        protected void AddInclude(string includePath)
        {
            // Проверяем, что путь не пустой и содержит только допустимые символы
            if (string.IsNullOrWhiteSpace(includePath))
                throw new ArgumentException("Include path cannot be empty", nameof(includePath));

            if (includePath.Any(c => !char.IsLetterOrDigit(c) && c != '.'))
                throw new ArgumentException("Include path contains invalid characters", nameof(includePath));

            // Разбиваем путь на части и проверяем каждую часть
            var parts = includePath.Split('.');
            var currentType = typeof(T);

            foreach (var part in parts)
            {
                var property = currentType.GetProperty(part);
                if (property == null)
                    throw new ArgumentException($"Property {part} not found on type {currentType.Name}");

                currentType = property.PropertyType;
            }

            IncludeStrings.Add(includePath);

            // Также создаем выражение для Includes
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression propertyExpression = parameter;

            foreach (var part in parts)
            {
                propertyExpression = Expression.Property(propertyExpression, part);
            }

            var lambda = Expression.Lambda<Func<T, object>>(
                Expression.Convert(propertyExpression, typeof(object)),
                parameter);

            Includes.Add(lambda);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderByExpression"></param>
        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderByDescendingExpression"></param>
        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> BuildCriteria(QueryParameters parameters)
        {
            Expression<Func<T, bool>> criteria = p => true;

            foreach (var filter in parameters.Filters)
            {
                var parts = filter.Value.Split(':');
                var value = parts.Length > 1 ? parts[1] : filter.Value;
                var operatorStr = parts.Length > 1 ? parts[0] : "eq";

                criteria = CombineCriteria(criteria, BuildSingleCriteria(filter.Key, operatorStr, value));
            }

            // Обработка диапазонных фильтров
            var rangeFilters = parameters.Filters
                .Where(f => f.Key.EndsWith("_from") || f.Key.EndsWith("_to"))
                .GroupBy(f => f.Key[..f.Key.LastIndexOf('_')]);

            foreach (var rangeGroup in rangeFilters)
            {
                var fromFilter = rangeGroup.FirstOrDefault(f => f.Key.EndsWith("_from"));
                var toFilter = rangeGroup.FirstOrDefault(f => f.Key.EndsWith("_to"));

                if (fromFilter.Value != null)
                {
                    criteria = CombineCriteria(criteria,
                        BuildSingleCriteria(rangeGroup.Key, "gte", fromFilter.Value));
                }

                if (toFilter.Value != null)
                {
                    criteria = CombineCriteria(criteria,
                        BuildSingleCriteria(rangeGroup.Key, "lte", toFilter.Value));
                }
            }

            return criteria;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> CombineCriteria(
        Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T));
            var combined = Expression.AndAlso(
                Expression.Invoke(left, parameter),
                Expression.Invoke(right, parameter));
            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyPath"></param>
        /// <param name="operatorStr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private static Expression<Func<T, bool>> BuildSingleCriteria(
            string propertyPath,
            string operatorStr,
            string value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression property = parameter;

            foreach (var member in propertyPath.Split('.'))
            {
                property = Expression.PropertyOrField(property, member);
            }

            var constant = GetConstantExpression(property.Type, value);

            Expression body;

            if (operatorStr == "contains")
            {
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                if (containsMethod == null)
                    throw new InvalidOperationException("Contains method not found");

                body = Expression.Call(property, containsMethod, constant);
            }

            body = operatorStr switch
            {
                "eq" => Expression.Equal(property, constant),
                "gt" => Expression.GreaterThan(property, constant),
                "lt" => Expression.LessThan(property, constant),
                "gte" => Expression.GreaterThanOrEqual(property, constant),
                "lte" => Expression.LessThanOrEqual(property, constant),
                _ => Expression.Equal(property, constant)
            };

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        private static Expression GetConstantExpression(Type type, string value)
        {
            if (type == typeof(string))
                return Expression.Constant(value);

            if (type == typeof(int))
                return Expression.Constant(int.Parse(value));

            if (type == typeof(decimal))
                return Expression.Constant(decimal.Parse(value));

            if (type == typeof(DateTime))
                return Expression.Constant(DateTime.Parse(value));

            if (type == typeof(bool))
                return Expression.Constant(bool.Parse(value));

            throw new NotSupportedException($"Type {type.Name} is not supported");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyPath"></param>
        /// <returns></returns>
        private Expression<Func<T, object>> CreateOrderExpression(string propertyPath)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression property = parameter;

            foreach (var member in propertyPath.Split('.'))
            {
                property = Expression.PropertyOrField(property, member);
            }

            var convert = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<T, object>>(convert, parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyPath"></param>
        /// <returns></returns>
        protected static bool PropertyExists(Type type, string propertyPath)
        {
            var currentType = type;
            foreach (var part in propertyPath.Split('.'))
            {
                var prop = currentType.GetProperty(part);
                if (prop == null) return false;
                currentType = prop.PropertyType;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="propertyPath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected static Expression GetPropertyExpression(ParameterExpression parameter, string propertyPath)
        {
            Expression property = parameter;
            foreach (var member in propertyPath.Split('.'))
            {
                var propInfo = property.Type.GetProperty(member);
                if (propInfo == null)
                    throw new ArgumentException($"Property {member} not found on type {property.Type.Name}");

                property = Expression.Property(property, propInfo);
            }
            return property;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyPath"></param>
        protected void AddIncludeForPath(string propertyPath)
        {
            if (string.IsNullOrWhiteSpace(propertyPath))
                return;

            var parts = propertyPath.Split('.');
            if (parts.Length <= 1)
                return;

            // Добавляем полный путь
            AddInclude(propertyPath);

            // Добавляем все промежуточные пути
            var currentPath = parts[0];
            AddInclude(currentPath);

            for (int i = 1; i < parts.Length - 1; i++)
            {
                currentPath += $".{parts[i]}";
                AddInclude(currentPath);
            }
        }
    }
}
