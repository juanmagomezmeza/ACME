using ACME.SchoolManagement.Core.Application.Entities;

namespace ACME.SchoolManagement.Core.Application.Contracts
{
    public interface ICourseService
    {
        Task<string> RegisterCourse(Course course);
    }
}
