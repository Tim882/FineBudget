using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class ValidationException : ApiException
    {
        public ValidationException(ErrorCode errorCode, string? additionalMessage = null) : base(errorCode, additionalMessage)
        {
        }
    }
}
