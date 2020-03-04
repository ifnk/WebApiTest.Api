using WebApiTest.Api.Controllers;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities.DatabaseEntities;

namespace WebApiTest.Api.Profile
{
    public class AnswerProfile : AutoMapper.Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AddAnswerDto>();
            CreateMap<AddAnswerDto, Answer>();
            
            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerDto, Answer>();
        }
    }
}