using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiTest.Api.Test
{
    public interface IEventTest
    {
        // 
        Task Raise(Guid userId);
        void RegisterEvent();
        Task LogoutEvent();
         Task EventMethod(Guid userId);
    }
}