using ACME.SchoolManagement.Core.Domain.Common;
using FluentValidation;

namespace ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse
{
    public class RegisterCourseCommandValidator : AbstractValidator<RegisterCourseCommand>
    {
        public RegisterCourseCommandValidator()
        {
            RuleFor(r => r.EndDate).NotNull().NotEmpty().WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNull, RegisterCourseConstants.EndDate));
            RuleFor(r => r.StartDate).NotNull().NotEmpty().WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNull, RegisterCourseConstants.StartDate));
            RuleFor(r => r.RegistrationFee).NotNull().GreaterThan(0).WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNonNumeric, RegisterCourseConstants.RegistrationFee));
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNull, RegisterCourseConstants.Name));
            RuleFor(r => r).Must(r => r.EndDate > r.StartDate).WithMessage(RegisterCourseValidationConstants.DatesValidations);
        }
    }
}
