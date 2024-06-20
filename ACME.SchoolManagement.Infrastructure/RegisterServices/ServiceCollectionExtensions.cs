using ACME.SchoolManagement.Core.Domain.Common;
using ACME.SchoolManagement.Core.Domain.Contracts.Request;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ACME.SchoolManagement.Infrastructure.RegisterServices
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRequestHandlers(this IServiceCollection services, Assembly assembly)
        {
            var handlerType = typeof(IRequestHandler<,>);
            var handlers = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType) && t.Namespace.Contains("ACME.SchoolManagement.Core.Application.Use_cases"))
                .ToList();

            foreach (var handler in handlers)
            {
                services.AddScoped(handler);
            }
        }

        public static void AddRepositories(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                                .Where(t => t.GetInterfaces().Any(i => i.Name == $"I{t.Name}") && t.Namespace == GeneralConstants.NamespacePersistenceDataAccess)
                                .ToList();

            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}");
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
        }

        public static void AddAppServices(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                                .Where(t => t.GetInterfaces().Any(i => i.Name == $"I{t.Name}") && t.Namespace == GeneralConstants.NamespaceServicesDataAccess)
                                .ToList();

            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}");
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
        }

        public static void AddValidatorServices(this IServiceCollection services, Assembly assembly)
        {
            var validatorType = typeof(IValidator<>);
            var validators = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == validatorType))
                .ToList();

            foreach (var validator in validators)
            {
                var interfaceType = validator.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == validatorType);
                services.AddScoped(interfaceType, validator);
            }
        }
    }
}
