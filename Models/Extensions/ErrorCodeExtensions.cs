using System.ComponentModel;

namespace Base.Models
{
    public static class ErrorCodeExtensions
    {
        // Получение текстового описания из атрибута [Description]
        public static string GetDescription(this ErrorCode errorCode)
        {
            var field = errorCode.GetType().GetField(errorCode.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute?.Description ?? errorCode.ToString();
        }

        // Создание объекта ошибки для возврата из API
        public static ApiResponse<object> ToErrorResponse(this ErrorCode errorCode, string? additionalInfo = null)
        {
            return new ApiResponse<object>
            {
                Message = errorCode.GetDescription(),
                ErrorCode = (int)errorCode,
                Success = false,
            };
        }

        // Бросаем исключение с этим кодом
        public static void Throw(this ErrorCode errorCode, string? additionalInfo = null)
        {
            throw new ApiException(errorCode, additionalInfo);
        }
    }
}
