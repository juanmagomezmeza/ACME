using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.Domain.Contracts.Services
{
    public interface ICourseService
    {
        Task<string> RegisterCourse(Course course);
    }
}
