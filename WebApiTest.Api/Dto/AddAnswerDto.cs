using System;

namespace WebApiTest.Api.Dto
{
    public class AddAnswerDto
    {
        public string Body { get; set; }
    }
    
    public class AnswerDto
    {
        
        public Guid Id { get; set; }
        public string Body { get; set; }
        public Guid QuestionId { get; set; }
    }
}