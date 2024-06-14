﻿using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Application.Contracts;
using ACME.SchoolManagement.Core.Application.Services;
using ACME.SchoolManagement.Infrastructure;
using ACME.SchoolManagement.Infrastructure.Data;
using ACME.SchoolManagement.Infrastructure.PaymentGateway;
using ACME.SchoolManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACME.SchoolManagement.Api.Installers
{
    /// <summary>
    /// Install app services
    /// </summary>
    public class ServicesInstaller : IInstaller
    {
        /// <summary>
        /// inicialize services
        /// </summary>
        /// <param name="services">service to init</param>
        /// <param name="configuration">configuration data</param>
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILoggerService>(provider => new SerilogLoggerService());
            services.AddDbContext<SchoolContext>(options => options.UseInMemoryDatabase("SchoolDb"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IPaymentGateway, PaymentGateway>();

        }
    }
}
