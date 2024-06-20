using ACME.SchoolManagement.Core.Domain.Contracts.Repositories;

namespace ACME.SchoolManagement.Core.Domain.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        IEnrollmentRepository Enrollments { get; }
        int Complete();
    }
}
