using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Domain.Contracts.Repositories;
using ACME.SchoolManagement.Persistence.Repositories;

namespace ACME.SchoolManagement.Api.Installers
{
    public class RepositoriesInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        }
    }
}
