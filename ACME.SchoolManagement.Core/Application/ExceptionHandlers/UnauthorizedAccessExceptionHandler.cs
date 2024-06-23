using ACME.SchoolManagement.Core.Domain.Contracts.Exceptions;
using System.Net;

namespace ACME.SchoolManagement.Core.Application.ExceptionHandlers
{
    public class UnauthorizedAccessExceptionHandler : IExceptionHandler
    {
        public Type ExceptionType => typeof(UnauthorizedAccessException);

        public (HttpStatusCode status, string message) HandleException(Exception exception)
        {
            return (HttpStatusCode.Unauthorized, "Unauthorized Access");
        }
    }
}
