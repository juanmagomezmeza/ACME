using ACME.SchoolManagement.Core.Entities;

namespace ACME.SchoolManagement.Core.Application.Contracts
{
    public interface ICourseService
    {
        Task<string> RegisterCourse(Course course);
    }
}
