using ACME.SchoolManagement.Core.Domain.Contracts.Exceptions;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using System.Net;

namespace ACME.SchoolManagement.Core.Application.ExceptionHandlers
{
    public class PaymentExceptionHandler : IExceptionHandler
    {
        public Type ExceptionType => typeof(PaymentException);

        public (HttpStatusCode status, string message) HandleException(Exception exception)
        {
            return (HttpStatusCode.PaymentRequired, exception.Message);
        }
    }
}
