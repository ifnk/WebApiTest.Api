using System;

namespace WebApiTest.Api.Entities.DatabaseEntities
{
    public class Answer : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public string Body { get; set; }
    }
}