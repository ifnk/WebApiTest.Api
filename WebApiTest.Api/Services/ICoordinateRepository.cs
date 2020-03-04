using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;

namespace WebApiTest.Api.Services
{
    public interface ICoordinateRepository : IBaseRepository<Coordinate>
    {
        bool AddSingleton(Coordinate coordinate);
    }
}