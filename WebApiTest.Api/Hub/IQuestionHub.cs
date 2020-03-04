using System;
using System.Threading.Tasks;

namespace WebApiTest.Api.Hub
{
    public interface IQuestionHub
    {
        Task QuestionScoreChange(Guid questionId, int score);
    }
}