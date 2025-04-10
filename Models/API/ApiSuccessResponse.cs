

namespace Base.Models
{
    public class ApiSuccessResponse<T>: ApiResponse<T>
    {
        public T? Data { get; set; }
    }
}
