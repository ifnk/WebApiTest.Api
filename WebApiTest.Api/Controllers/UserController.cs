using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Services;

namespace WebApiTest.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }


        //api/users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<MsgReturn<object>>> GetUsers([FromQuery] QueryParameter queryParameter)
        {
            if (queryParameter == null) return Ok(new MsgReturn<string>() {Success = false, Msg = "没有传查询参数!"});
            var users = await _userRepository.GetUsersAsync(queryParameter);
            return Ok(new MsgReturn<object>
            {
                Msg = "查询用户列表成功",
                Response = new
                {
                    List = users,
                    totalCount = users.TotalItemsCount,
                    pageIndex = users.PageIndex,
                    pageSize = users.PageSize
                }
            });
        }

        //api/users/123
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(Guid userId)
        {
            var user = await _userRepository.GetUserAsync(userId);

            if (user == null) return NotFound();
            return Ok(user);
        }

        //[Authorize]
        [HttpPost("login")]
        //api/users/login
        public async Task<ActionResult<MsgReturn<object>>> Login(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var res = await _userRepository.GetUserByNameAsync(user.Name);
            if (res != null)
            {
                if (res.Password != user.Password)
                    return Ok(new MsgReturn<object> {Msg = "密码错误!", Success = false});
                string token = GetToken();
                return Ok(new MsgReturn<object>
                {
                    Msg = "登录成功!",
                    Response = new
                    {
                        token = token,
                        user = res
                    }
                });
            }

            return Ok(new MsgReturn<object> {Msg = "用户不存在!", Success = false});
        }

        //生成token 的方法 
        public string GetToken()
        {
            SecurityToken securityToken = new JwtSecurityToken(
                //这个要保证 和 startup 里面 的 jwt 配置 一样，不一样的话 就没有办法 认证了
                issuer: "chenNingJwtApi", //发行人
                audience: "chenNingJwtApp", //订阅人
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("chenNingJsonWebToken")),
                    SecurityAlgorithms.HmacSha256), //认证密钥
                expires: DateTime.Now.AddHours(3), //过期日期 3小时后过期  
                claims: new Claim[] { } //在这里定义 角色 认证 
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken);


            return jwtToken;
        }
    }
}