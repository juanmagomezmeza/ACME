using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Entities;
using ACME.SchoolManagement.Infrastructure.Data;

namespace ACME.SchoolManagement.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolContext context) : base(context) { }
    }
}
