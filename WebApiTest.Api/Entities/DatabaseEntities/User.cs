using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApiTest.Api.Entities.DatabaseEntities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; } = true;
        public ICollection<Coordinate> Coordinates { get; set; }
    }
}