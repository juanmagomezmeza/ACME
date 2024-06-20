using System.Text.Json.Serialization;
using ACME.SchoolManagement.Api.Filters;
using ACME.SchoolManagement.Api.Installers.Contracts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ACME.SchoolManagement.Api.Installers
{
    /// <summary>
    /// Instala la configuracion de los controllers
    /// </summary>
    public class MvcInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            // Configuración de controllers con FluentValidation y JSON options
            services.AddControllers()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>())
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });

            // Agregar HttpContextAccessor si es necesario
            services.AddHttpContextAccessor();
        }
    }
}
