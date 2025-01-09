using AutoMapper;
using MyAppApplication.DTOs;
using MyAppDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppInfrastructure.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, GetEmpDto>().ReverseMap();
            CreateMap<PostEmpDto, Employee>().ReverseMap();
            CreateMap<Employee, PostEmpDto>();
            CreateMap<Employee, PutEmpDto>().ReverseMap();
        }
    }
}
