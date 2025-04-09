
namespace Base.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? Description { get; set; }
        public int ErrorCode { get; set; }
        public T? Data { get; set; }

        // Успешный ответ
        public static ApiResponse<T> Ok(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message ?? "Операция выполнена успешно"
            };
        }

        // Ошибка
        public static ApiResponse<T> Fail(int errorCode, string message, string? description = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode,
                Description = description,
            };
        }
    }
}
