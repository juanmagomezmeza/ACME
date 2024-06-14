using System.Text.Json.Serialization;
using ACME.SchoolManagement.Api.Filters;
using ACME.SchoolManagement.Api.Installers.Contracts;
using FluentValidation.AspNetCore;

namespace ACME.SchoolManagement.Api.Installers
{
    /// <summary>
    /// Instala la configuracion de los controllers
    /// </summary>
    public class MvcInstaller : IInstaller
    {
        [Obsolete]
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers().AddFluentValidation().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddMvc(
                config =>
                {
                    config.Filters.Add(typeof(GlobalExceptionFilter));
                });

            services.AddHttpContextAccessor();
        }
    }
}
