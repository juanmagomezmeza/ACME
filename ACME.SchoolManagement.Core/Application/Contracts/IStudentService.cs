using ACME.SchoolManagement.Core.Application.Entities;

namespace ACME.SchoolManagement.Core.Application.Contracts
{
    public interface IStudentService
    {
        Task<string> RegisterStudent(Student student);

    }
}
