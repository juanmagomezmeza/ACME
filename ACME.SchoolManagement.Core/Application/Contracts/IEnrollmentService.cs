using ACME.SchoolManagement.Core.Application.Entities;

namespace ACME.SchoolManagement.Core.Application.Contracts
{
    public interface IEnrollmentService
    {
        Task<string> Enroll(Enrollment enrollment);
        Task<List<Enrollment>> ListOfCoursesAndStudentsByDate(DateTime startDate, DateTime endDate);
        Task<bool> CourseExistsAsync(Guid courseID);
        Task<bool> StudentExistsAsync(Guid studentID);
    }
}
