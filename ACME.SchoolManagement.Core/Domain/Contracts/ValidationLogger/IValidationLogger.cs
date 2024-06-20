using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using FluentValidation.Results;

namespace ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger
{
    public interface IValidationLogger
    {
        string LogValidationErrors<T>(IRequest<T> request, IEnumerable<ValidationFailure> failures);
    }
}
