using ACME.SchoolManagement.Core.Domain.Common;
using FluentValidation;

namespace ACME.SchoolManagement.Core.Application.Use_cases.ListOfCoursesAndStudentsByDate
{
    public class ListOfCoursesAndStudentsByDateQueryValidator : AbstractValidator<ListOfCoursesAndStudentsByDateQuery>
    {
        public ListOfCoursesAndStudentsByDateQueryValidator()
        {
            RuleFor(r => r.StartDate).NotNull().WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNull, ListOfCoursesAndStudentsByDateConstants.StartDate));
            RuleFor(x => x.EndDate).NotNull().WithMessage(string.Format(GenericValidationMessages.GenericNotEmptyOrNull, ListOfCoursesAndStudentsByDateConstants.EndDate));
        }
    }
}
