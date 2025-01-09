using MyAppDomain.Entities;
using MyAppDomain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppDomain.Interfaces
{
    public interface IEmpRepo
    {
        Task<ServiceResponse<List<Employee>>>GetEmployees();
        Task<ServiceResponse<Employee>> AddEmployeeAsync(Employee entity);
        Task<Employee> UpdateEmployeeAsync(Guid employeeId, Employee entity);
        Task<bool> DeleteEmployeeAsync(Guid employeeId);
    }
}
