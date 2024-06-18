using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Application.Services.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Infrastructure;
using System.Reflection;

namespace ACME.SchoolManagement.Api.Installers
{
    /// <summary>
    /// Install use cases
    /// </summary>
    public class HandlersInstaller : IInstaller
    {
        /// <summary>
        /// Install use cases
        /// </summary>
        /// <param name="services">services to install</param>
        /// <param name="configuration">configuration info</param>
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRequestDispatcher, RequestDispatcher>();

            services.AddScoped(provider =>
            {
                var serviceProvider = provider.GetService<IServiceProvider>();
                var logger = provider.GetService<ILoggerService>();
                return new RequestDispatcher(serviceProvider, logger);
            });
            services.AddRequestHandlers(Assembly.Load("ACME.SchoolManagement.Core"));
        }
    }
}
