using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Entities;
using ACME.SchoolManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ACME.SchoolManagement.Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext context) : base(context) { }

    }
}
