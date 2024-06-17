using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Models;

namespace ACME.SchoolManagement.Core.Application.Use_cases.ListOfCoursesAndStudentsByDate
{
    public class ListOfCoursesAndStudentsByDateQuery : IRequest<IList<EnrollmentModel>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
