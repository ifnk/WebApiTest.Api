using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Api.Dapper;
using WebApiTest.Api.Data;
using WebApiTest.Api.Entities.DatabaseEntities;
using WebApiTest.Api.Repositories;
using WebApiTest.Api.Services;

namespace WebApiTest.Api.Test
{
    // 定义 委托 传 string ,类型 是 void  
    public delegate Task RaiseEventHandler(Guid userId);

    public class EventTest : IEventTest
    {
        private static bool IsDoSomeThing { get; set; }

        //定义  事件
        public event RaiseEventHandler RaiseEvent;

        // 
        public async Task Raise(Guid userId)
        {
            await Task.Run(() => { RaiseEvent?.Invoke(userId); });
        }

        public void RegisterEvent()
        {
            IsDoSomeThing = true;
            RaiseEvent += EventMethod; // 订阅事件
        }

        public async Task LogoutEvent()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("注销事件");
                IsDoSomeThing = false;
            });
        }

        // 注册热键 
        public async Task EventMethod(Guid userId)
        {
            await Task.Run(() =>
            {
                if (!IsDoSomeThing)
                {
                    return;
                }

                CoordinateDapperDAL coordinateDapperDal = new CoordinateDapperDAL();

                Console.WriteLine("注册事件，开始执行方法");

                var i = 1;
                do
                {
                    coordinateDapperDal.Insert();
                    Console.WriteLine($"{i++}");
                } while (IsDoSomeThing);
            });
        }
    }
}