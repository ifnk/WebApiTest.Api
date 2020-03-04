using System;

namespace WebApiTest.Api.Dto
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Score { get; set; } = 0;
        public int AnswerCount { get; set; }
    }
}