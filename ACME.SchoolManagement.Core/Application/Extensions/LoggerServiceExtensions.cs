using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using FluentValidation.Results;

namespace ACME.SchoolManagement.Core.Application.Extensions
{
    public static class LoggerServiceExtensions
    {
        public static string LogValidationErrors<T>(this ILoggerService logger, IRequest<T> request, IEnumerable<ValidationFailure> failures)
        {
            var requestName = request.GetType().Name;
            var errors = failures.ToErrorString();
            var message = $"Request : {requestName}, contains validation Errors: {errors}";
            logger.Error(message, errors);
            return message;
        }

        public static string? LogRequestException<T>(this ILoggerService logger, IRequest<T> request, Exception ex, bool logValidationException = false)
        {
            if ((logValidationException || ex is not RequestValidationException) && logger != null)
            {
                var requestName = request.GetType().Name;
                var message = $"{requestName} Error: {ex.ToString()}";
                logger.Error(message, request);
                return message;
            }
            return null;
        }
    }
}
