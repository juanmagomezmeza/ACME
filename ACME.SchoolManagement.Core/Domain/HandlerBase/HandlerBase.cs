using ACME.SchoolManagement.Core.Application.Extensions;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Exceptions;
using FluentValidation;

namespace ACME.SchoolManagement.Core.Domain.HandlerBase
{
    public abstract class HandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ILoggerService _logger;
        private readonly IValidator<TRequest> _validator;

        protected HandlerBase(ILoggerService logger, IValidator<TRequest> validator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
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
                string message = _logger?.LogValidationErrors(request, failures);
                throw new RequestValidationException(message, failures);
            }
        }
    }
}
