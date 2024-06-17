using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.Domain.Contracts.Repositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        List<Enrollment> ListOfCoursesAndStudentsByDate(DateTime startDate, DateTime endDate);
        Task<bool> CourseExists(Guid courseID);
        Task<bool> StudentExists(Guid studentID);
    }
}
