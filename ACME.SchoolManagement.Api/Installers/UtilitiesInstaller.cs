using ACME.SchoolManagement.Api.Installers.Contracts;

namespace ACME.SchoolManagement.Api.Installers
{
    /// <summary>
    /// Get info from configuration file
    /// </summary>
    public class UtilitiesInstaller : IInstaller
    {
        /// <summary>
        /// Install services
        /// </summary>
        /// <param name="services">services to install</param>
        /// <param name="configuration">app configuration</param>
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped(provider => configuration.GetSection("AzureKeyVault").Get<AzureKeyVaultSettings>());
        }
    }   
}
