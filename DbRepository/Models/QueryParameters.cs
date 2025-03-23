namespace FineBudget.Models
{
    public class QueryParameters
    {
        // Пагинация
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Фильтрация
        public string Filter { get; set; }
        public string FilterValue { get; set; }

        // Сортировка
        public string SortBy { get; set; }
        public bool SortDescending { get; set; }
    }
}
