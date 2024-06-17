using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.Domain.Contracts.Services
{
    public interface IEnrollmentService
    {
        Task<string> Enroll(Enrollment enrollment);
        Task<List<Enrollment>> ListOfCoursesAndStudentsByDate(DateTime startDate, DateTime endDate);
        Task<bool> CourseExistsAsync(Guid courseID);
        Task<bool> StudentExistsAsync(Guid studentID);
    }
}
