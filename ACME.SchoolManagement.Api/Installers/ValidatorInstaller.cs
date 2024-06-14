using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse;
using FluentValidation;

namespace ACME.SchoolManagement.Api.Installers
{
    /// <summary>
    /// Install validations
    /// </summary>
    public class ValidatorInstaller : IInstaller
    {
        /// <summary>
        /// Install services validacions
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="configuration">configuration data</param>
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidator<ContractCourseCommand>, ContractCourseCommandValidator>();
        }
    }
}
