﻿using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Core.Domain.Contracts.UnitOfWork;
using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.Application.Services.DataAccess
{
    public class EnrollmentService : IEnrollmentService, IService<Enrollment>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Enroll(Enrollment enrollment)
        {
            _unitOfWork.Enrollments.Add(enrollment);
            _unitOfWork.Complete();
            return await Task.FromResult("Enrollment registered");
        }

        public async Task<List<Enrollment>> ListOfCoursesAndStudentsByDate(DateTime startDate, DateTime endDate)
        {
            var enrollments = _unitOfWork.Enrollments.ListOfCoursesAndStudentsByDate(startDate, endDate);
            return await Task.FromResult(enrollments);
        }

        public async Task<bool> CourseExistsAsync(Guid courseID)
        {
            return await _unitOfWork.Enrollments.CourseExists(courseID);
        }

        public async Task<bool> StudentExistsAsync(Guid studentID)
        {
            return await _unitOfWork.Enrollments.StudentExists(studentID);
        }

        public IEnumerable<Enrollment> GetAll()
        {
            return _unitOfWork.Enrollments.GetAll();
        }

        public Enrollment GetById(int id)
        {
            return _unitOfWork.Enrollments.GetById(id);
        }

        public void Save(Enrollment entity)
        {
            _unitOfWork.Enrollments.Add(entity);
            _unitOfWork.Complete();
        }

        public void Update(Enrollment entity)
        {
            _unitOfWork.Enrollments.Update(entity);
            _unitOfWork.Complete();
        }

        public void Delete(Enrollment entity)
        {
            _unitOfWork.Enrollments.Delete(entity);
            _unitOfWork.Complete();
        }
    }
}
