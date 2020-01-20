using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Api.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string EmployeeNo { get; set; }
        public string Name { get; set; }


        public String GenderDisplay { get; set; } //字符串性别

        public int Age { get; set; } // 出生日期 改成 年龄
        // public CompanyDto CompanyDto { get; set; } // 每一个查询 出来 的员工 都带一个 company 信息 有点冗余了……
    }
}
