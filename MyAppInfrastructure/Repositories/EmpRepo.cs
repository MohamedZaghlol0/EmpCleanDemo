using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAppApplication.DTOs;
using MyAppDomain.Entities;
using MyAppDomain.Helper;
using MyAppDomain.Interfaces;
using MyAppInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyAppInfrastructure.Repositories.EmpRepo;

namespace MyAppInfrastructure.Repositories
{

    public class EmpRepo(AppDBcontext dbContext, IMapper _mapper) : IEmpRepo
    {
        public async Task<ServiceResponse<List<Employee>>> GetEmployees()
        {
            var serviceResponse = new ServiceResponse<List<Employee>>();
            var allemps = await dbContext.Employees.ToListAsync();
            serviceResponse.Data = allemps;
            return serviceResponse;
                 
        }
        public async Task<ServiceResponse<Employee>> AddEmployeeAsync(Employee entity)
        {
            var serviceResponse = new ServiceResponse<Employee>();

            try
            {
                if (entity == null)
                {
                    serviceResponse.Code = "400";
                    serviceResponse.Message = "Invalid request: Employee data cannot be null.";
                    serviceResponse.Error = "Null Employee entity.";
                    return serviceResponse;
                }
                entity.Id = Guid.NewGuid();
                await dbContext.Employees.AddAsync(entity);
                await dbContext.SaveChangesAsync();
                serviceResponse.Data = entity;
                serviceResponse.Code = "200";
                serviceResponse.Message = "Employee added successfully.";
            }
            catch (DbUpdateException dbEx)
            {
                serviceResponse.Code = "500";
                serviceResponse.Message = "A database error occurred while adding the employee.";
                serviceResponse.Error = dbEx.InnerException?.Message ?? dbEx.Message;
            }
            catch (Exception ex)
            {
                serviceResponse.Code = "500";
                serviceResponse.Message = "An error occurred while processing the request.";
                serviceResponse.Error = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<Employee> UpdateEmployeeAsync(Guid employeeId, Employee updatedProperties)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);

            if (employee is not null)
            {
                if (!string.IsNullOrEmpty(updatedProperties.Name))
                    employee.Name = updatedProperties.Name;

                if (!string.IsNullOrEmpty(updatedProperties.Email))
                    employee.Email = updatedProperties.Email;

                if (!string.IsNullOrEmpty(updatedProperties.Phone))
                    employee.Phone = updatedProperties.Phone;

                await dbContext.SaveChangesAsync();

                return employee;
            }

            throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
        }


        public async Task<bool> DeleteEmployeeAsync(Guid employeeId)
        {
            bool isfound = false;
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee is not null)
            {
                dbContext.Employees.Remove(employee);
                isfound = true;
                await dbContext.SaveChangesAsync();
            }

            return isfound;
        }

    }

}
