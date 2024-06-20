using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Application.Logger;
using ACME.SchoolManagement.Core.Domain.Common;
using ACME.SchoolManagement.Core.Domain.Contracts.ValidationLogger;
using ACME.SchoolManagement.Infrastructure.RegisterServices;
using System.Reflection;

namespace ACME.SchoolManagement.Api.Installers
{
    public class ValidatorServicesInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidationLogger, ValidationLogger>();
            services.AddValidatorServices(Assembly.Load(GeneralConstants.CoreAssembly));
        }
    }
}
