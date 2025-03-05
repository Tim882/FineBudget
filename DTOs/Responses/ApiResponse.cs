using System;
namespace DTOs.Responses
{
    public class ApiResponse<T>
    {
        public Guid ResultCode { get; set; } = 0;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}

