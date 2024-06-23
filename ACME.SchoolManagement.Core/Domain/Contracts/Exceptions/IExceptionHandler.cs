using System.Net;

namespace ACME.SchoolManagement.Core.Domain.Contracts.Exceptions
{
    public interface IExceptionHandler
    {
        (HttpStatusCode status, string message) HandleException(Exception exception);
        Type ExceptionType { get; }
    }
}
