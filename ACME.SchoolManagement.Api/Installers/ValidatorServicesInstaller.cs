﻿using ACME.SchoolManagement.Api.Installers.Contracts;
using ACME.SchoolManagement.Core.Domain.Common;
using ACME.SchoolManagement.Infrastructure;
using System.Reflection;

namespace ACME.SchoolManagement.Api.Installers
{
    public class ValidatorServicesInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorServices(Assembly.Load(GeneralConstants.CoreAssembly));
        }
    }
}
