using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Entities;

namespace ACME.SchoolManagement.Core.Application.Services
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
