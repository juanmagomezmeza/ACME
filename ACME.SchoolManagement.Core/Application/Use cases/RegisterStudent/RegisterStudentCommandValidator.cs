using ACME.SchoolManagement.Core.Domain.Common;
using FluentValidation;

namespace ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent
{
    public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
    {
        public RegisterStudentCommandValidator()
        {
            RuleFor(r => r.Age).NotNull().GreaterThan(0).WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNonNumeric, RegisterStudentConstants.Age));
            RuleFor(r => r).Must(r => r.Age > 17).WithMessage(RegisterStudentValidationConstants.AgeGreatherThan17);
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNull, RegisterStudentConstants.Name));
        }
    }
}
