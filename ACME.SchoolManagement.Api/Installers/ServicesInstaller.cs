using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Domain.Contracts.Services;
using ACME.SchoolManagement.Infrastructure.LoggerService;
using ACME.SchoolManagement.Infrastructure.PaymentGateway;

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
            services.AddScoped<IPaymentGateway, PaymentGateway>();
        }
    }
}
