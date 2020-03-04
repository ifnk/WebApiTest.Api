using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApiTest.Api.Hub
{
    public class QuestionHub : Hub<IQuestionHub>
    {
    }
}