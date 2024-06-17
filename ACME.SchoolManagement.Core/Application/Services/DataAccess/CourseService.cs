using ACME.SchoolManagement.Core.Domain.Contracts.Repositories;
using ACME.SchoolManagement.Core.Domain.Contracts.UnitOfWork;
using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.Application.Services.DataAccess
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> RegisterCourse(Course course)
        {
            _unitOfWork.Courses.Add(course);
            _unitOfWork.Complete();
            return await Task.FromResult("Course registered");
        }
    }
}
