using AutoMapper;
using MediatR;
using MyAppApplication.DTOs;
using MyAppDomain.Entities;
using MyAppDomain.Helper;
using MyAppDomain.Interfaces;

using System.ComponentModel.DataAnnotations;


namespace MyAppApplication.Commands
{
    public record AddEmpCommand(PostEmpDto Emp) : IRequest<ServiceResponse<PostEmpDto>>;

    public class AddEmpCommandHandler : IRequestHandler<AddEmpCommand, ServiceResponse<PostEmpDto>>
    {
        private readonly IEmpRepo _empRepo;
        private readonly IMapper _mapper;

        public AddEmpCommandHandler(IEmpRepo empRepo, IMapper mapper)
        {
            _empRepo = empRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PostEmpDto>> Handle(AddEmpCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<PostEmpDto>();

            if (request.Emp == null)
            {
                response.Code = "400";
                response.Message = "Invalid request: Employee data cannot be null.";
                response.Error = "Null Employee DTO";
                return response;
            }

            try
            {
                var employeeEntity = _mapper.Map<Employee>(request.Emp);
                var serviceResponse = await _empRepo.AddEmployeeAsync(employeeEntity);
                if (serviceResponse.Data == null)
                {
                    response.Code = "500";
                    response.Message = "Failed to save the employee to the database.";
                    response.Error = "Repository returned null for the Data property.";
                    return response;
                }
                response.Data = _mapper.Map<PostEmpDto>(serviceResponse.Data);
                response.Code = serviceResponse.Code ?? "200";
                response.Message = serviceResponse.Message ?? "Employee added successfully.";

                return response;
            }
            catch (ValidationException ex)
            {
                response.Code = "422";
                response.Message = "Validation error occurred.";
                response.ValidationError = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Code = "500";
                response.Message = "An error occurred while processing the request.";
                response.Error = ex.Message;
                return response;
            }
        }


    }
}