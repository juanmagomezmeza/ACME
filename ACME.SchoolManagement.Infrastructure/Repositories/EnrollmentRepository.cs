using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Entities;
using ACME.SchoolManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ACME.SchoolManagement.Infrastructure.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(SchoolContext context) : base(context) { }

        public List<Enrollment> ListOfCoursesAndStudentsByDate(DateTime startDate, DateTime endDate)
        {
            var enrollmentsWithDetails = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.Course.StartDate >= startDate && e.Course.EndDate <= endDate)
                .ToList();

            return enrollmentsWithDetails;
        }

        public async Task<bool> CourseExists(Guid courseID)
        {
            return await _context.Enrollments.AnyAsync(s => s.CourseID == courseID);
        }

        public async Task<bool> StudentExists(Guid studentID)
        {
            return await _context.Enrollments.AnyAsync(s => s.StudentID == studentID);
        }
    }
}
