using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.UnitOfWork;
using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.Application.Services.DataAccess
{
    public class StudentService : IStudentService, IService<Student>
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(Student entity)
        {
            _unitOfWork.Students.Delete(entity);
            _unitOfWork.Complete();
        }

        public IEnumerable<Student> GetAll()
        {
            return _unitOfWork.Students.GetAll();
        }

        public Student GetById(int id)
        {
            return _unitOfWork.Students.GetById(id);
        }

        public async Task<string> RegisterStudent(Student student)
        {
            _unitOfWork.Students.Add(student);
            _unitOfWork.Complete();
            return await Task.FromResult("Student registered");
        }

        public void Save(Student entity)
        {
            _unitOfWork.Students.Add(entity);
            _unitOfWork.Complete();
        }

        public void Update(Student entity)
        {
            _unitOfWork.Students.Update(entity);
            _unitOfWork.Complete();
        }
    }
}
