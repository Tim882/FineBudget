using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ErrorsHandlers
{
    public class ApiException : Exception
    {
        public ErrorCode ErrorCode { get; }
        public string? AdditionalMessage { get; }

        public ApiException(ErrorCode errorCode, string? additionalMessage = null)
            : base($"{errorCode.GetDescription()}. {additionalMessage}".Trim())
        {
            ErrorCode = errorCode;
            AdditionalMessage = additionalMessage;
        }

        // Преобразование в ProblemDetails (для ASP.NET Core)
        public ProblemDetails ToProblemDetails()
        {
            return new ProblemDetails
            {
                Title = ErrorCode.GetDescription(),
                Detail = Message,
                Status = (int)GetHttpStatusCode()
            };
        }

        private HttpStatusCode GetHttpStatusCode() => ErrorCode switch
        {
            ErrorCode.BadRequest => HttpStatusCode.BadRequest,
            ErrorCode.Unauthorized => HttpStatusCode.Unauthorized,
            ErrorCode.Forbidden => HttpStatusCode.Forbidden,
            ErrorCode.NotFound => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };
    }
}
