using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Entities;

namespace ACME.SchoolManagement.Core.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> RegisterStudent(Student student)
        {
            _unitOfWork.Students.Add(student);
            _unitOfWork.Complete();
            return await Task.FromResult("Student registered");
        }
    }
}
