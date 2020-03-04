using System;
using System.Linq;
using Dapper;
using WebApiTest.Api.Entities.DatabaseEntities;

namespace WebApiTest.Api.Dapper
{
    // 坐标表的数据库操作类
    public class CoordinateDapperDAL
    {
        //新增
        public void Insert()
        {
            using var connection = ConnectionFactory.GetOpenConnection();
            connection.Query<string>(
                @"insert into Coordinates(Id,X,Y,CreateTime,UserId,IsRemoved) values (@Id,@X,@Y,@CreateTime,@UserId,@IsRemoved);",
                new Coordinate
                {
                    Id = Guid.NewGuid(),
                    X = 10,
                    Y = 10,
                    UserId = Guid.Parse("DC359997-B795-44DF-9820-9516E60B8F9C"),
                    CreateTime = DateTime.Now,
                    IsRemoved = false
                }
            );
        }
    }
}