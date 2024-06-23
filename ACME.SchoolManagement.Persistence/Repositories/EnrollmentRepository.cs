using ACME.SchoolManagement.Core.Domain.Contracts.Repositories;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Persistence.BaseRepository;
using ACME.SchoolManagement.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ACME.SchoolManagement.Persistence.Repositories
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
            return await _context.Courses.AnyAsync(s => s.CourseID == courseID);
        }

        public async Task<bool> StudentExists(Guid studentID)
        {
            return await _context.Students.AnyAsync(s => s.StudentID == studentID);
        }
    }
}
