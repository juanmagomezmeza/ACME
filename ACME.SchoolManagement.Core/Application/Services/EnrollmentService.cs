using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Entities;

namespace ACME.SchoolManagement.Core.Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Enroll(Enrollment enrollment)
        {
            _unitOfWork.Enrollments.Add(enrollment);
            _unitOfWork.Complete();
            return await Task.FromResult("Enrollment registered");
        }

        public async Task<List<Enrollment>> ListOfCoursesAndStudentsByDate(DateTime startDate, DateTime endDate)
        {
            var enrollments = _unitOfWork.Enrollments.ListOfCoursesAndStudentsByDate(startDate, endDate);
            return await Task.FromResult<List<Enrollment>>(enrollments);
        }

        public async Task<bool> CourseExistsAsync(Guid courseID)
        {
            return await _unitOfWork.Enrollments.CourseExists(courseID);
        }

        public async Task<bool> StudentExistsAsync(Guid studentID)
        {
            return await _unitOfWork.Enrollments.StudentExists(studentID);
        }
    }
}
