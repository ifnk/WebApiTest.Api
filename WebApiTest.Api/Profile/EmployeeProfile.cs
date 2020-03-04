using System;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;

namespace WebApiTest.Api.Profile
{
    public class EmployeeProfile : AutoMapper.Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName}{src.LastName}"))
                .ForMember(dest => dest.GenderDisplay,
                    opt => opt.MapFrom(src => src.Gender.ToString())
                )
                .ForMember(dest => dest.CompanyName,
                    opt => opt.MapFrom(src => src.Company.Name)
                )
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year)
                );
            CreateMap<EmployeeAddDto, Employee>();
            CreateMap<Employee, EmployeeAddDto>();
            CreateMap<Employee, EmployeeUpdateDto>();
            CreateMap<EmployeeUpdateDto, Employee>();
        }
    }
}