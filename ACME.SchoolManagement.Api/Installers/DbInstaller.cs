using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Domain.Contracts.UnitOfWork;
using ACME.SchoolManagement.Persistence.Contexts;
using ACME.SchoolManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACME.SchoolManagement.Api.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SchoolContext>(options => options.UseInMemoryDatabase("SchoolDb"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
