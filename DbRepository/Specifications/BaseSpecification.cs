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

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        // Конструктор, принимающий QueryParameters
        public BaseSpecification(QueryParameters parameters)
        {
            // Фильтрация
            if (!string.IsNullOrEmpty(parameters.Filter) && !string.IsNullOrEmpty(parameters.FilterValue))
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, parameters.Filter); // Доступ к свойству
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var filterValue = Expression.Constant(parameters.FilterValue, typeof(string));

                var containsExpression = Expression.Call(property, containsMethod, filterValue);
                Criteria = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
            }

            // Сортировка
            if (!string.IsNullOrEmpty(parameters.SortBy))
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, parameters.SortBy); // Доступ к свойству

                if (parameters.SortDescending)
                {
                    ApplyOrderByDescending(Expression.Lambda<Func<T, object>>(property, parameter));
                }
                else
                {
                    ApplyOrderBy(Expression.Lambda<Func<T, object>>(property, parameter));
                }
            }

            // Пагинация
            if (parameters.PageNumber > 0 && parameters.PageSize > 0)
            {
                ApplyPaging((parameters.PageNumber - 1) * parameters.PageSize, parameters.PageSize);
            }
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
