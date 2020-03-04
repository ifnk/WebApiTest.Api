using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Api.Data;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;
using WebApiTest.Api.Services;

namespace WebApiTest.Api.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }


        //获取所有的user 表
        public async Task<PaginatedList<User>> GetUsersAsync([FromQuery] QueryParameter queryParameter)
        {
            if (queryParameter == null) throw new ArgumentNullException(nameof(queryParameter));
            var query = GetAll();
            if (!string.IsNullOrEmpty(queryParameter.Key))
            {
                string key = "%" + queryParameter.Key.ToLowerInvariant() + "%";
                query = query.Where(x => EF.Functions.Like(x.Name, key));
            }

            return await PaginatedList<User>.CreateAsync(query, queryParameter.PageIndex, queryParameter.PageSize);
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            return userId == Guid.Empty
                ? throw new ArgumentNullException(nameof(userId))
                : await Context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            return userName == null
                ? throw new ArgumentNullException(nameof(userName))
                : await Context.Users.FirstOrDefaultAsync(x => x.Name == userName);
        }


        public void UpdateUser(User user)
        {
        }

        public void DeleteUser(User User)
        {
            if (User == null) throw new ArgumentNullException(nameof(User));
            Context.Users.Remove(User);
        }

        public async Task<bool> UserExistAsync(Guid userId)
        {
            return userId == Guid.Empty
                ? throw new ArgumentNullException(nameof(userId))
                : await Context.Users.AnyAsync(x => x.Id == userId);
        }

        public async Task<bool> UserExistByNameAsync(string userName)
        {
            return userName == null
                ? throw new ArgumentNullException(nameof(userName))
                : await Context.Users.AnyAsync(x => x.Name == userName);
        }

    }
}