using System;
using WebApiTest.Api.Entities;

namespace WebApiTest.Api.Dto
{
    public class EmployeeAddDto
    {
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }



        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }



    }
}