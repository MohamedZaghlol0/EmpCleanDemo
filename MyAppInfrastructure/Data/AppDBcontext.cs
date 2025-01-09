using Microsoft.EntityFrameworkCore;
using MyAppDomain.Entities;

namespace MyAppInfrastructure.Data
{
    public class AppDBcontext(DbContextOptions<AppDBcontext> options) :DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
