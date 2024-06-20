using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Domain.Common;
using ACME.SchoolManagement.Infrastructure.RegisterServices;
using System.Reflection;

namespace ACME.SchoolManagement.Api.Installers
{
    public class RepositoriesInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(Assembly.Load(GeneralConstants.PersistenceAssembly));
        }
    }
}
