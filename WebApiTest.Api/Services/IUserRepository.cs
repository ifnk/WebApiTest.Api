using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTest.Api.Entities;

namespace WebApiTest.Api.Services
{
    public interface IUserRepository

    {
        //公司相关 的接口方法 
        Task<PaginatedList<User>> GetUsersAsync(QueryParameter queryParameter);
        Task<User> GetUserAsync(Guid userId);
        Task<User> GetUserByNameAsync(string userName);
        void AddUser(User User);
        void UpdateUser(User User);
        void DeleteUser(User User);
        Task<bool> UserExistAsync(Guid userId);

        Task<bool> UserExistByNameAsync(string userName);


        //操作完成后 要保存下……
        Task<bool> SaveAsync();
    }
}