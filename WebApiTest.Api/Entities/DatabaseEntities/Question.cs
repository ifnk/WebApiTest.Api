using System.Collections.Generic;

namespace WebApiTest.Api.Entities.DatabaseEntities
{
    public class Question : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int Score { get; set; } = 0;
        public List<Answer> Answers { get; set; }
    }
}