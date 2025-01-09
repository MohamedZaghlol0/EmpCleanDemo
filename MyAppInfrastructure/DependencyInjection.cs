using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyAppDomain.Interfaces;
using MyAppInfrastructure.Data;
using MyAppInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<AppDBcontext>(options =>
            options.UseSqlServer("Server=MOHAMED;Database=CleanArchDemo;Trusted_Connection=True;TrustServerCertificate=True;\r\n"));

            services.AddScoped<IEmpRepo, EmpRepo>();
            return services;
        }
    }
}
