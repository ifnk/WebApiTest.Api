using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTest.Api.Entities;

namespace WebApiTest.Api.Dto
{
    public class CompanyDto  //返回给app 的资源 
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
    }
}