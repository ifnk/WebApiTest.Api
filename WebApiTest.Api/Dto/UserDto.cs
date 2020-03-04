using System;

namespace WebApiTest.Api.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; } = true;
    }
}