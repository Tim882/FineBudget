
namespace Base.Models
{
    public class ApiFailureResponse<T>: ApiResponse<T>
    {
        public int ErrorCode { get; set; }
    }
}
