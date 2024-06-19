using ACME.SchoolManagement.Core.Domain.Contracts.Repositories;
using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Persistence.BaseRepository;
using ACME.SchoolManagement.Persistence.Contexts;

namespace ACME.SchoolManagement.Persistence.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(SchoolContext context) : base(context) { }
}
