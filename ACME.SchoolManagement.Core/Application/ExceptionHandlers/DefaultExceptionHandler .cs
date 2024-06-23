using ACME.SchoolManagement.Core.Domain.Contracts.Exceptions;
using System.Net;

namespace ACME.SchoolManagement.Core.Application.ExceptionHandlers
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public Type ExceptionType => typeof(Exception);

        public (HttpStatusCode status, string message) HandleException(Exception exception)
        {
            return (HttpStatusCode.InternalServerError, "An internal error occurred.");
        }
    }
}
