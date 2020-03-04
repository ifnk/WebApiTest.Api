using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;

namespace WebApiTest.Api.Dto
{
    public abstract class EmployeeAddOrUpdateDto
    {
        [Display(Name = "员工号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string EmployeeNo { get; set; }

        [Display(Name = "firstName")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string FirstName { get; set; }

        [Display(Name = "lastName")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string LastName { get; set; }

        [Display(Name = "性别")]
        [Required(ErrorMessage = "{0}不能为空")]
        public Gender Gender { get; set; }

        [Display(Name = "出生日期")]
        [Required(ErrorMessage = "{0}不能为空")]
        public DateTime DateOfBirth { get; set; }


        //当 上面  自带的 属性 不通过 ， 就不走这了 ，当 上面 自带的属性 通过 的时候  才会走 自定义的 错误属性 
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (validationContext == null) throw new ArgumentNullException(nameof(validationContext));
            if (FirstName == LastName)
                yield return new ValidationResult(
                    "姓和名不能一样",
                    new[] {nameof(FirstName), nameof(LastName)});
        }


    }
}