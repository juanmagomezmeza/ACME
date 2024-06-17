using ACME.SchoolManagement.Core.Domain.Contracts.Request;

namespace ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse
{
    public class ContractCourseCommand : IRequest<string?>
    {
        public Guid CourseID { get; set; }
        public Guid StudentID { get; set; }
    }
}
