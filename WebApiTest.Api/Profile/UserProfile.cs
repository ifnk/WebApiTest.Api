using WebApiTest.Api.Controllers;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;

namespace WebApiTest.Api.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}