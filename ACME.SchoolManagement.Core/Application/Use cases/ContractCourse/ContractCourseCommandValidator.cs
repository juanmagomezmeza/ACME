using ACME.SchoolManagement.Core.Application.Common;
using FluentValidation;

namespace ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse
{
    public class ContractCourseCommandValidator : AbstractValidator<ContractCourseCommand>
    {
        public ContractCourseCommandValidator()
        {
            RuleFor(r => r.CourseID).NotNull().NotEmpty().WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNull, ContractCourseConstants.CourseID));
            RuleFor(e => e.StudentID);
            RuleFor(x => x.StudentID).NotNull().NotEmpty().WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNull, ContractCourseConstants.StudentID));
            RuleFor(e => e.CourseID);
        }
    }
}
