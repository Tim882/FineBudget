namespace FineBudget.Models
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
    }
}
