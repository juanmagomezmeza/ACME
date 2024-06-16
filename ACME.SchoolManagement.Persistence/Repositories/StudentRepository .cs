using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Entities;
using ACME.SchoolManagement.Persistence.Contexts;

namespace ACME.SchoolManagement.Persistence.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext context) : base(context) { }

    }
}
