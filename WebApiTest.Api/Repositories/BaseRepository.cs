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
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext Context;

        protected BaseRepository(AppDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(T model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            Context.Set<T>().Add(model);
        }

        public void Update(T model)
        {
        }

        public void Remove(T model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            Context.Set<T>().Remove(model);
        }

        public async Task<bool> SaveAsync()
        {
            // 返回值应该是返回数据库中受影响记录的行数。
            // 对于增加，返回的是增加成功的行数。
            // 对于更新，返回的时候成功更新了几行。
            // 对于删除，返回到底成功删除了几行。
            return await Context.SaveChangesAsync() > 0;
        }

        public bool Save()
        {
            // 返回值应该是返回数据库中受影响记录的行数。
            // 对于增加，返回的是增加成功的行数。
            // 对于更新，返回的时候成功更新了几行。
            // 对于删除，返回到底成功删除了几行。
            return Context.SaveChanges() > 0;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return id == Guid.Empty
                ? throw new ArgumentNullException(nameof(id))
                : await Context.Set<User>().AnyAsync(x => x.Id == id);
        }


        public async Task<T> GetOneByIdAsync(Guid id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>()
                .Where(x => !x.IsRemoved)
                .AsQueryable();
        }
    }
}