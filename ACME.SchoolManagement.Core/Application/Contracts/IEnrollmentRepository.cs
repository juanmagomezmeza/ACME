using ACME.SchoolManagement.Core.Application.Entities;

namespace ACME.SchoolManagement.Core.Application.Contracts
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        List<Enrollment> ListOfCoursesAndStudentsByDate(DateTime startDate, DateTime endDate);
        Task<bool> CourseExists(Guid courseID);
        Task<bool> StudentExists(Guid studentID);
    }
}
