using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Api.Data;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Services;

namespace WebApiTest.Api.Repostories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<User>> GetUsersAsync(QueryParameter queryParameter)
        {
            var query = _context.Users.AsQueryable();


            if (!string.IsNullOrEmpty(queryParameter.Key))
            {
                string key = "%" + queryParameter.Key.ToLowerInvariant().Trim() + "%";
                query = query.Where(x => EF.Functions.Like(x.Name, key));
            }

            var count = await query.CountAsync();

            var data = await query
                .Skip((queryParameter.PageIndex - 1) * queryParameter.PageSize)
                .Take(queryParameter.PageSize)
                .ToListAsync();

            return new PaginatedList<User>(queryParameter.PageIndex, queryParameter.PageSize, count, data);
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            return userId == Guid.Empty
                ? throw new ArgumentNullException(nameof(userId))
                : await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            return userName == null
                ? throw new ArgumentNullException(nameof(userName))
                : await _context.Users.FirstOrDefaultAsync(x => x.Name == userName);
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            else
            {
                user.Id = Guid.Empty;
                _context.Users.Add(user);
            }
        }

        public void UpdateUser(User user)
        {
        }

        public void DeleteUser(User user)
        {
            if (user != null) _context.Users.Remove(user);
            throw new ArgumentNullException(nameof(user));
        }

        public async Task<bool> UserExistAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _context.Users.AnyAsync(x => x.Id == userId);
        }


        public async Task<bool> UserExistByNameAsync(string userName)
        {
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            return await _context.Users.AnyAsync(x => x.Name == userName);
        }


        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}