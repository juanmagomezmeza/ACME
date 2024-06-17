using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.Domain.Contracts.Repositories
{
    public interface ICourseService
    {
        Task<string> RegisterCourse(Course course);
    }
}
