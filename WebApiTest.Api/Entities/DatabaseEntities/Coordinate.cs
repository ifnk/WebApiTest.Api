using System;

namespace WebApiTest.Api.Entities.DatabaseEntities
{
    public  class Coordinate : BaseEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}