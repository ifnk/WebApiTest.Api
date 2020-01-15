using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Api.Entities
{
    // 将 查询参数 封装 成 一个类
    public class QueryParameter
    {
        //当前 页数 
        public int PageIndex { get; set; } = 1;

        //每页 数量
        public int PageSize { get; set; } = 10;
        public string Key { get; set; }
    }
}