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
                options.UseSqlServer(
                    "Data Source=.; User ID=Sa; Password=@Qwe112233; Initial Catalog=CleanArchDemo; MultipleActiveResultSets=True; MultiSubnetFailover=True; TrustServerCertificate=True; Connection Timeout=30;"));


            services.AddScoped<IEmpRepo, EmpRepo>();
            return services;
        }
    }
}