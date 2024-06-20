using ACME.SchoolManagement.Api.Filters;
using ACME.SchoolManagement.Api.Installers.Contracts;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace ACME.SchoolManagement.Api.Installers
{
    /// <summary>
    /// Instala la configuracion de los controllers
    /// </summary>
    public class MvcInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddFluentValidationAutoValidation();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "School Management", Description = "Management", Version = "v1" });
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
