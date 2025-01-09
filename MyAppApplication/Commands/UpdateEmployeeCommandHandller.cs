using AutoMapper;
using MediatR;
using MyAppApplication.DTOs;
using MyAppDomain.Entities;
using MyAppDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppApplication.Commands
{
    public record UpdateEmployeeCommand(Guid EmployeeId, PutEmpDto PutEmp) : IRequest<PutEmpDto>;

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, PutEmpDto>
    {
        private readonly IEmpRepo _empRepo;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IEmpRepo empRepo, IMapper mapper)
        {
            _empRepo = empRepo;
            _mapper = mapper;
        }

        public async Task<PutEmpDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeEntity = _mapper.Map<Employee>(request.PutEmp);
            var updatedEmployee = await _empRepo.UpdateEmployeeAsync(request.EmployeeId, employeeEntity);
            return _mapper.Map<PutEmpDto>(updatedEmployee);
        }
    }
}
