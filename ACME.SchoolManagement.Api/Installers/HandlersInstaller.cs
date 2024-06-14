using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Application.Request;
using ACME.SchoolManagement.Core.Application.Use_cases.ContractCourse;
using System.Reflection;
using ACME.SchoolManagement.Infrastructure;
using ACME.SchoolManagement.Core.Application.Use_cases.RegisterStudent;
using ACME.SchoolManagement.Core.Application.Use_cases.RegisterCourse;
using ACME.SchoolManagement.Core.Application.Use_cases.ListOfCoursesAndStudentsByDate;

namespace ACME.SchoolManagement.Api.Installers
{
    /// <summary>
    /// Install use cases
    /// </summary>
    public class HandlersInstaller : IInstaller
    {
        /// <summary>
        /// Install use cases
        /// </summary>
        /// <param name="services">services to install</param>
        /// <param name="configuration">configuration info</param>
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ServiceFactory>(serviceProvider => {
                return type => serviceProvider.GetService(type);
            });
            services.AddScoped<RequestDispatcher>();
            services.AddScoped<IRequestDispatcher, RequestDispatcher>();
            services.AddRequestHandlers(typeof(ContractCourseCommandHandler).GetTypeInfo().Assembly);
            services.AddRequestHandlers(typeof(RegisterStudentCommandHandler).GetTypeInfo().Assembly);
            services.AddRequestHandlers(typeof(RegisterCourseCommandHandler).GetTypeInfo().Assembly);
            services.AddRequestHandlers(typeof(ListOfCoursesAndStudentsByDateQueryHandler).GetTypeInfo().Assembly);

        }
    }
}
