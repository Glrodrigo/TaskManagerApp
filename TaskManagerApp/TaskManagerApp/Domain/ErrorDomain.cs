using Microsoft.AspNetCore.Mvc;

namespace TaskManagerApp.Domain
{
    public class ErrorDomain
    {
        public string ErrorCode { get; set; }
        public string ErrorType { get; set; }

        public ErrorDomain(string errorCode, string errorType)
        {
            ErrorCode = errorCode;
            ErrorType = errorType;
        }
    }
}
