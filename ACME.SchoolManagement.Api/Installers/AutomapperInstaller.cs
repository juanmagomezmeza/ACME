using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Domain.Common;
using System.Reflection;

namespace ACME.SchoolManagement.Api.Installers
{
    public class AutomapperInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper( Assembly.Load(GeneralConstants.CoreAssembly));
        }
    }
}
