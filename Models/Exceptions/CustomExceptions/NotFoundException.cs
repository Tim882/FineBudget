using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class NotFoundException: ApiException
    {
        public NotFoundException(ErrorCode errorCode, string? additionalMessage = null) : base(errorCode, additionalMessage)
        {
        }
    }
}
