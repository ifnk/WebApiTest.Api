using System;
using System.Collections.Generic;

namespace WebApiTest.Api.Entities.DatabaseEntities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}