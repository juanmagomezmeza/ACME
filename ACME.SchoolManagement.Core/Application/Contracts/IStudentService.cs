using ACME.SchoolManagement.Core.Entities;

namespace ACME.SchoolManagement.Core.Application.Contracts
{
    public interface IStudentService
    {
        Task<string> RegisterStudent(Student student);

    }
}
