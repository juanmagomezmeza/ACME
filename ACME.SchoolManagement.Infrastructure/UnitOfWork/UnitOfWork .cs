using ACME.SchoolManagement.Core.Domain.Contracts.Repositories;
using ACME.SchoolManagement.Core.Domain.Contracts.UnitOfWork;
using ACME.SchoolManagement.Persistence.Contexts;
using ACME.SchoolManagement.Persistence.Repositories;

namespace ACME.SchoolManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _context;

        public UnitOfWork(SchoolContext context)
        {
            _context = context;
            Students = new StudentRepository(_context);
            Courses = new CourseRepository(_context);
            Enrollments = new EnrollmentRepository(_context);
        }

        public IStudentRepository Students { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IEnrollmentRepository Enrollments { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
