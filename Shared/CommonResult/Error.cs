using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CommonResult
{
    public class Error
    {

        public string Code { get;  }
        public string Description { get; }
        public ErrorType Type { get;  }

        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }
        public static Error Failure(string Code="General.Failure",string description ="A General Failure Has Occurred")
        {
           return new Error(Code, description, ErrorType.Failure);
        }
        public static Error Validation(string code = "General.Validation", string description = "A Validation Error Has Occurred")
        {
            return new Error(code, description, ErrorType.validation);
        }
        public static Error NotFound(string code = "General.NotFound", string description = "The Requested Resource is not Found")
        {
            return new Error(code, description, ErrorType.NotFound);
        }
        public static Error Unauthorized(string code = "General.Unauthorized", string description = "You aren't authorize To do this action")
        {
            return new Error(code, description, ErrorType.Unauthorized);
        }
        public static Error Forbidden(string code = "General.Forbidden", string description = "You aren't have permission to do this action")
        {
            return new Error(code, description, ErrorType.Forbidden);
        }
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "The Provider Credentials is not Correct")
        {
            return new Error(code, description, ErrorType.InvalidCredentials);
        }
        public static Error Conflict(string code = "General.Conflict", string description = "The Ticket is already booked")
        {
            return new Error(code, description, ErrorType.Conflict);
        }
        public static Error PaymentFailed(string code = "General.PaymentFailed", string description = "The Payment Process Failed")
        {
            return new Error(code, description, ErrorType.PaymentFailed);
        }
    }
}
