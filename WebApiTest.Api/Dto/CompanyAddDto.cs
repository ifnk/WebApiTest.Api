using System.Collections.Generic;

namespace WebApiTest.Api.Dto
{
    public class CompanyAddDto
    {
        public string Name { get; set; }

        public string Introduction { get; set; }


        public ICollection<EmployeeAddDto> EmployeedAddDtos { get; set; } = new List<EmployeeAddDto>();
    }
}