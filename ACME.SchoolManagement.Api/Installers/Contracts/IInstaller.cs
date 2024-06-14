namespace ACME.SchoolManagement.Api.Installers.Contracts
{
    /// <summary>
    /// Interface for installer
    /// </summary>
    public interface IInstaller
    {
        /// <summary>
        /// Implements installers
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        void InstallService(IServiceCollection services, IConfiguration configuration);
    }
}
