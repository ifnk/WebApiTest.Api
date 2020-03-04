using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTest.Api.Entities;

namespace WebApiTest.Api.Services
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department> AddAsync(Department department);
    }
}