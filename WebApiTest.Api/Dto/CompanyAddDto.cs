using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiTest.Api.Dto
{
    public class CompanyAddDto //传进来 的 添加 模型 
    {
        [Display(Name = "公司名")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "{0}的长度不能小于1")]
        public string Name { get; set; }

        [Display(Name = "简介")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "{0}的长度不能小于1")]
        public string Introduction { get; set; }


        public ICollection<EmployeeAddDto> EmployeedAddDtos { get; set; } = new List<EmployeeAddDto>();
    }
}