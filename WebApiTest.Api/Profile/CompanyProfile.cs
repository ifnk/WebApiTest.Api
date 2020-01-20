using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities;

namespace WebApiTest.Api.Profile
{
    //建立 model 和 modelDto 之间 的映射关系 
    public class CompanyProfile : AutoMapper.Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.CompanyName,
                    opt => opt.MapFrom(src => src.Name)
                );
            CreateMap<CompanyAddDto, Company>()
                .ForMember(
                    dest => dest.Employees, opt => opt.MapFrom(src => src.EmployeedAddDtos))
                ;
            CreateMap<Company, CompanyAddDto>()
                .ForMember(dest => dest.EmployeedAddDtos, opt => opt.MapFrom(src => src.Employees));
        }
    }
}