using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Entities;
using ACME.SchoolManagement.Persistence.Contexts;

namespace ACME.SchoolManagement.Persistence.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(SchoolContext context) : base(context) { }
}
