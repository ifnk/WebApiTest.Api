namespace WebApiTest.Api.Controllers
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; } = true;
    }
}