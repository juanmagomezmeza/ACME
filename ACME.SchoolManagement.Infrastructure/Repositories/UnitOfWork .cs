﻿using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Infrastructure.Data;

namespace ACME.SchoolManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _context;

        public UnitOfWork(SchoolContext context)
        {
            _context = context;
            Students = new StudentRepository(_context);
            Courses = new CourseRepository(_context);
            Enrollments = new EnrollmentRepository(_context);
        }

        public IStudentRepository Students { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IEnrollmentRepository Enrollments { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
