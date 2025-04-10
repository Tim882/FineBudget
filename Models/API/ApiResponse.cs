
namespace Base.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StatusCode { get; set; }

        // Успешный ответ
        public static ApiResponse<T> Ok(T data, string message = "")
        {
            return new ApiSuccessResponse<T>
            {
                Success = true,
                Data = data,
                Message = string.IsNullOrEmpty(message) ? "Операция выполнена успешно" : message
            };
        }

        // Ошибка
        public static ApiResponse<T> Fail(int errorCode, string message, string? description = null)
        {
            return new ApiFailureResponse<T>
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode,
                Description = string.IsNullOrEmpty(description) ? "Произошла неизвестная ошибка" : description,
            };
        }
    }
}
