using Microsoft.Extensions.Options;
using WebApiTest.Api.Controllers;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities.DatabaseEntities;

namespace WebApiTest.Api.Profile
{
    public class QuestionProfile : AutoMapper.Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.AnswerCount,
                    opt
                        => opt.MapFrom
                            (src => src.Answers.Count))
                ;
            CreateMap<QuestionDto, Question>();

            CreateMap<Question, AddQuestionDto>();
            CreateMap<AddQuestionDto, Question>();
        }
    }
}