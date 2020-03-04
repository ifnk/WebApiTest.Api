using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiTest.Api.Entities;

namespace WebApiTest.Api.Services
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        //创建 
        void Add(T model);

        //修改 
        void Update(T model);

        //删除 对象   
        void Remove(T model);

        // 保存 
        Task<bool> SaveAsync();
        bool Save();
        Task<bool> ExistByIdAsync(Guid id);

        //获取 一个 根据 id 
        Task<T> GetOneByIdAsync(Guid id);

        //获取 获取多个(列表)
        IQueryable<T> GetAll();
    }
}