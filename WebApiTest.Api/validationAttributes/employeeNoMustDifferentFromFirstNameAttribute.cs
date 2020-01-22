using System.ComponentModel.DataAnnotations;
using WebApiTest.Api.Dto;

namespace WebApiTest.Api.validationAttributes
{
    public class EmployeeNoMustDifferentFromFirstNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addDto = (EmployeeAddDto) validationContext.ObjectInstance;
            if (addDto.EmployeeNo == addDto.FirstName)
            {
                return new ValidationResult(ErrorMessage, new[] {nameof(EmployeeAddDto)});
            }

            return ValidationResult.Success; //情况不符合 就返回  验证通过 
        }
    }
}