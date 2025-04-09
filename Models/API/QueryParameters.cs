using Microsoft.AspNetCore.Http;

namespace Base.Models
{
    public class QueryParameters
    {
        // Пагинация
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Сортировка
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }

        // Фильтрация
        public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();

        public static QueryParameters Parse(HttpRequest request)
        {
            var parameters = new QueryParameters();

            // Пагинация
            if (int.TryParse(request.Query["page"], out var page))
                parameters.PageNumber = page;

            if (int.TryParse(request.Query["pageSize"], out var pageSize))
                parameters.PageSize = pageSize;

            // Сортировка
            parameters.SortBy = request.Query["sortBy"].FirstOrDefault();
            parameters.SortDescending = bool.Parse(request.Query["sortDescending"].FirstOrDefault() ?? "false");

            // Фильтры
            foreach (var (key, value) in request.Query)
            {
                if (key == "PageNumber" || key == "PageSize" || key == "SortBy" || key == "SortDescending")
                    continue;

                if (key.EndsWith("_gte") || key.EndsWith("_lte") ||
                    key.EndsWith("_gt") || key.EndsWith("_lt") ||
                    key.EndsWith("_contains"))
                {
                    var operatorPart = key.Split('_').Last();
                    var field = key.Substring(0, key.LastIndexOf('_'));
                    parameters.Filters.Add(field, $"{operatorPart}:{value}");
                }
                else if (key.EndsWith("_from") || key.EndsWith("_to"))
                {
                    // Обработка диапазонов
                    var rangeType = key.Split('_').Last();
                    var field = key.Substring(0, key.LastIndexOf('_'));
                    parameters.Filters.Add($"{field}_{rangeType}", value);
                }
                else
                {
                    // Простое равенство
                    parameters.Filters.Add(key, value);
                }
            }

            return parameters;
        }
    }
}
