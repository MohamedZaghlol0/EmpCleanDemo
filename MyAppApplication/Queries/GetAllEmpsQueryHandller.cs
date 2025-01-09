using AutoMapper;
using MediatR;
using MyAppApplication.DTOs;
using MyAppDomain.Entities;
using MyAppDomain.Helper;
using MyAppDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppApplication.Queries
{
    public record GetAllEmpsQuery() : IRequest<ServiceResponse<List<GetEmpDto>>>;


    public class GetAllEmpsQueryHandller : IRequestHandler<GetAllEmpsQuery, ServiceResponse<List<GetEmpDto>>>
    {
        private readonly IEmpRepo _empRepo;
        private readonly IMapper _mapper;

        public GetAllEmpsQueryHandller(IEmpRepo empRepo, IMapper mapper)
        {
            _empRepo = empRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetEmpDto>>> Handle(GetAllEmpsQuery request, CancellationToken cancellationToken)
        {
            var serviceResponse = new ServiceResponse<List<GetEmpDto>>();
            var repoResponse = await _empRepo.GetEmployees();
            serviceResponse.Data = _mapper.Map<List<GetEmpDto>>(repoResponse.Data);
            return serviceResponse;
        }
    }
}
