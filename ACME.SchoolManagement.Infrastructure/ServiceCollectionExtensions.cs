using ACME.SchoolManagement.Core.Application.Request;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ACME.SchoolManagement.Core.Application.Extensions;

namespace ACME.SchoolManagement.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRequestHandlers(this IServiceCollection services, Assembly assembly)
        {
            try
            {
                var allTypes = assembly.GetTypes();
                var handlers = (typeof(IRequestHandler<,>)).GetImplementedInterfacesFromTypes(allTypes);
                foreach (var hdlr in handlers)
                    services.AddScoped(hdlr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
