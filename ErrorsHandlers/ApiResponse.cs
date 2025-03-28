using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorsHandlers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? AdditionalMessage { get; set; }
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
        public static ApiResponse<T> Fail(ErrorCode errorCode, string? additionalMessage = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = errorCode.GetDescription(),
                ErrorCode = (int)errorCode,
                AdditionalMessage = additionalMessage,
            };
        }
    }
}
