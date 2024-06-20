using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using FluentValidation.Results;

namespace ACME.SchoolManagement.Core.Application.Logger
{
    public class ValidationLogger : IValidationLogger
    {
        private readonly ILoggerService _logger;

        public ValidationLogger(ILoggerService logger)
        {
            _logger = logger;
        }

        public string LogValidationErrors<T>(IRequest<T> request, IEnumerable<ValidationFailure> failures)
        {
            var requestName = request.GetType().Name;
            var errors = failures.ToErrorString();
            var message = $"Request : {requestName}, contains validation Errors: {errors}";
            _logger.Error(message, errors);
            return message;
        }
    }
}
