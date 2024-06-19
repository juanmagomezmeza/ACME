using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.UnitOfWork;
using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.Application.Services.DataAccess
{
    public class CourseService : ICourseService,IService<Course>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(Course entity)
        {
            _unitOfWork.Courses.Delete(entity);
            _unitOfWork.Complete();
        }

        public IEnumerable<Course> GetAll()
        {
            return _unitOfWork.Courses.GetAll();
        }

        public Course GetById(int id)
        {
            return _unitOfWork.Courses.GetById(id);
        }

        public async Task<string> RegisterCourse(Course course)
        {
            _unitOfWork.Courses.Add(course);
            _unitOfWork.Complete();
            return await Task.FromResult("Course registered");
        }

        public void Save(Course entity)
        {
            _unitOfWork.Courses.Add(entity);
            _unitOfWork.Complete();
        }

        public void Update(Course entity)
        {
            _unitOfWork.Courses.Update(entity);
            _unitOfWork.Complete();
        }
    }
}
