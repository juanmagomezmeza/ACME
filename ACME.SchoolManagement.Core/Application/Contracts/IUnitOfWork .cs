namespace ACME.SchoolManagement.Core.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        IEnrollmentRepository Enrollments { get; }
        int Complete();
    }
}
