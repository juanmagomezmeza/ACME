using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Application.Mappings;

namespace ACME.SchoolManagement.Api.Installers
{
    public class AutomapperInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(RegisterStudentMappings));
            services.AddAutoMapper(typeof(RegisterCourseMappings));
            services.AddAutoMapper(typeof(ContractCourseMappings));
            services.AddAutoMapper(typeof(ListOfCoursesAndStudentsByDateMappings));
        }
    }
}
