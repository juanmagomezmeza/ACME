using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.Domain.Contracts.Services
{
    public interface IStudentService
    {
        Task<string> RegisterStudent(Student student);

    }
}
