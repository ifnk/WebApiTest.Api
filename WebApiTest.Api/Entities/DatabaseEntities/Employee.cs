using System;

// 员工
namespace WebApiTest.Api.Entities.DatabaseEntities
{
    public class Employee : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Company Company { get; set; }
    }
}