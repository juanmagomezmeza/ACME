using ACME.SchoolManagement.Api.Filters;
using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Application.ExceptionHandlers;
using ACME.SchoolManagement.Core.Domain.Contracts.Exceptions;
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
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddFluentValidationAutoValidation();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "School Management", Description = "Management", Version = "v1" });
            });
            // Registrar manejadores de excepción del ensamblado ACME.SchoolManagement.Core
            var coreAssembly = typeof(IExceptionHandler).Assembly;
            var exceptionHandlers = coreAssembly.GetExportedTypes()
                                                .Where(t => typeof(IExceptionHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                                                .Select(Activator.CreateInstance)
                                                .Cast<IExceptionHandler>();

            foreach (var handler in exceptionHandlers)
            {
                services.AddTransient(handler.GetType());
                services.AddTransient<IExceptionHandler>(sp => sp.GetRequiredService(handler.GetType()) as IExceptionHandler);
            }

            // Registrar DefaultExceptionHandler
            services.AddSingleton<DefaultExceptionHandler>();

            // Registrar el filtro global y sus dependencias
            services.AddSingleton<GlobalExceptionFilter>();

            // Otros servicios
            services.AddControllers(options =>
            {
                options.Filters.AddService<GlobalExceptionFilter>();
            });
        }
    }
}
