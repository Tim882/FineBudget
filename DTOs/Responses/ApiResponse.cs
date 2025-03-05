using System;
namespace DTOs.Responses
{
    public class ApiResponse<T>
    {
        public Guid ResultCode { get; set; } = Guid.Empty;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}

