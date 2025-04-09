using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;

using Base.Models;

namespace Base.API
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;
            _logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exceptionMessage, DateTime.UtcNow);

            if (exception is ApiException)
            {
                ApiException apiException = (ApiException) exception;
                await httpContext.Response.WriteAsJsonAsync(
                    ApiResponse<object>.Fail(
                    (int)apiException.ErrorCode,
                    apiException.AdditionalMessage));

                return true;
            }
            else
            {
                await httpContext.Response.WriteAsJsonAsync(ApiResponse<object>.Fail(
                    (int)ErrorCode.InternalError, 
                    ErrorCode.InternalError.GetDescription()));
                
                return true;
            }
        }
    }
}
