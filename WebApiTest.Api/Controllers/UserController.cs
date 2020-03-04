using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApiTest.Api.Dto;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;
using WebApiTest.Api.Services;

namespace WebApiTest.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        //api/users  post请求 添加 用户 
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MsgReturn<object>>> AddUser([FromBody] User user)
        {
            if (await _userRepository.UserExistByNameAsync(user.Name))
                return Ok(new MsgReturn<object> {Msg = "该用户名已存在!", Success = false});
            for (int i = 0; i < 3; i++)
            {
                _userRepository.Add(new User()
                {
                    Name = "nono",
                    Password= "wudi"
                });
                await _userRepository.SaveAsync();
                
            }

            // if (!await _userRepository.SaveAsync())
            //     return Ok(new MsgReturn<object> {Msg = "未知原因，没有添加上用户", Success = false});
            return Ok(new MsgReturn<object> {Msg = "添加用户成功!",});
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<ActionResult<MsgReturn<object>>> DeleteUser(Guid userId)
        {
            var userEntity = await _userRepository.GetUserAsync(userId);
            if (userEntity == null) return Ok(new MsgReturn<object> {Msg = "没有找到要删除的用户!", Success = false});
            _userRepository.DeleteUser(userEntity);
            if (!await _userRepository.SaveAsync())
                return Ok(new MsgReturn<object> {Msg = "删除用户失败……", Success = false});
            return Ok(new MsgReturn<object> {Msg = "删除用户成功!"});
        }

        //api/users
        // [Authorize]
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
                    totalCount = users.TotalCount,
                    pageIndex = users.PageIndex,
                    pageSize = users.PageSize
                }
            });
        }

        [Authorize]
        [HttpPatch("{userId}")]
        public async Task<ActionResult<User>> UpdateUser(Guid userId, UserUpdateDto userUpdateDto)
        {
            if (userId == Guid.Empty) return Ok(new MsgReturn<string> {Msg = "没有传userId", Success = false});
            var user = await _userRepository.GetUserAsync(userId);
            if (!await _userRepository.UserExistAsync(userId))
                return Ok(new MsgReturn<string> {Msg = "没有找到对应id 的用户!", Success = false});
            _mapper.Map(userUpdateDto, user);
            if (await _userRepository.UserExistByNameAsync(user.Name))
                return Ok(new MsgReturn<string> {Msg = "用户名已存在", Success = false});
            _userRepository.UpdateUser(user);
            if (!await _userRepository.SaveAsync())
                return Ok(new MsgReturn<string> {Msg = "用户没有发生更改!", Success = false});
            return Ok(new MsgReturn<string> {Msg = "修改用户成功!"});
        }

        //api/users/123/status/{status}
        [Authorize]
        [HttpPut("{userId}/status/{status}")]
        public async Task<ActionResult<User>> ChangeUserStatus(Guid userId, bool status)
        {
            var user = await _userRepository.GetUserAsync(userId);
            if (user == null) return NotFound();
            user.Status = status;
            if (!await _userRepository.SaveAsync())
                return Ok(new MsgReturn<string> {Success = false, Msg = "更新用户状态未成功"});
            return Ok(new MsgReturn<string> {Msg = "更新用户状态成功!"});
        }

        //api/users/123
        // [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(Guid userId)
        {
            if (userId == Guid.Empty) return Ok(new MsgReturn<string> {Success = false, Msg = "没有传入userId!"});
            var user = await _userRepository.GetUserAsync(userId);
            if (user == null) return NotFound();
            return Ok(new MsgReturn<User> {Msg = "获取单个用户成功!", Response = user});
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
                var token = GetToken();
                return Ok(new MsgReturn<object>
                {
                    Msg = "登录成功!",
                    Response = new
                    {
                        token,
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
                "chenNingJwtApi", //发行人
                "chenNingJwtApp", //订阅人
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("chenNingJsonWebToken")),
                    SecurityAlgorithms.HmacSha256), //认证密钥
                expires: DateTime.Now.AddHours(3), //过期日期 3小时后过期  
                claims: new Claim[] { } //在这里定义 角色 认证 
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken);


            return jwtToken;
        }
    }
}