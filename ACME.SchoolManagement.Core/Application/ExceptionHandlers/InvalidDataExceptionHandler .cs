using ACME.SchoolManagement.Core.Domain.Contracts.Exceptions;
using System.Net;

namespace ACME.SchoolManagement.Core.Application.ExceptionHandlers
{
    public class InvalidDataExceptionHandler : IExceptionHandler
    {
        public Type ExceptionType => typeof(InvalidDataException);

        public (HttpStatusCode status, string message) HandleException(Exception exception)
        {
            return (HttpStatusCode.BadRequest, exception.Message);
        }
    }
}
