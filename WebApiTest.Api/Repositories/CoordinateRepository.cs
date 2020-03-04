using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Api.Data;
using WebApiTest.Api.Entities.DatabaseEntities;
using WebApiTest.Api.Services;

namespace WebApiTest.Api.Repositories
{
    public class CoordinateRepository : BaseRepository<Coordinate>, ICoordinateRepository
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public CoordinateRepository(AppDbContext context, DbContextOptions<AppDbContext> options) : base(context)
        {
            _options = options;
        }

        // ASP.NET Core 新建线程 task 中使用依赖注入的问题
        public bool AddSingleton(Coordinate coordinate)
        {
            using var context = new AppDbContext(_options);
            context.Set<Coordinate>().Add(coordinate);
            return context.SaveChanges() > 0;
        }
    }
}