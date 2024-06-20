using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Application.Logger;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using FluentValidation;

namespace ACME.SchoolManagement.Core.Domain.HandlerBase
{
    public abstract class HandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ILoggerService _logger;
        private readonly IValidator<TRequest> _validator;
        private readonly IValidationLogger _validationLogger;

        protected HandlerBase(ILoggerService logger, IValidator<TRequest> validator, IValidationLogger validationLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _validationLogger = validationLogger ?? throw new ArgumentNullException(nameof(validationLogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);
            return await HandleRequest(request, cancellationToken);
        }

        protected abstract Task<TResponse> HandleRequest(TRequest request, CancellationToken cancellationToken);

        private void ValidateRequest(TRequest request)
        {
            var validationResults = _validator.Validate(request);
            if (!validationResults.IsValid)
            {
                var failures = validationResults.Errors.ToList();
                var message = _validationLogger.LogValidationErrors(request, failures);
                throw new RequestValidationException(message, failures);
            }
        }
    }
}
