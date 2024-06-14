using ACME.SchoolManagement.Core.Application.Models;
using ACME.SchoolManagement.Core.Application.Request;

namespace ACME.SchoolManagement.Core.Application.Use_cases.ListOfCoursesAndStudentsByDate
{
    public class ListOfCoursesAndStudentsByDateQuery : IRequest<IList<EnrollmentModel>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
