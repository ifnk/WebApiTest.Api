using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiTest.Api.Entities;
using WebApiTest.Api.validationAttributes;

namespace WebApiTest.Api.Dto
{
    [EmployeeNoMustDifferentFromFirstName(ErrorMessage = "员工编号和姓名不能相同!")] //这个既可以作用与类级别 也可以作用 于 属性 级别 
    public class EmployeeAddDto : EmployeeAddOrUpdateDto
    {
    }
}