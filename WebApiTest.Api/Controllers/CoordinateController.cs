using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Api.Dapper;
using WebApiTest.Api.Entities;
using WebApiTest.Api.Entities.DatabaseEntities;
using WebApiTest.Api.Services;
using WebApiTest.Api.Test;

namespace WebApiTest.Api.Controllers
{
    [ApiController]
    // [Authorize]
    [Route("api/coordinate")]
    public class CoordinateController : Controller
    {
        private readonly IEventTest _eventTest;
        private readonly ICoordinateRepository _coordinateRepository;

        public CoordinateController(
            IEventTest eventTest,
            ICoordinateRepository coordinateRepository
        )
        {
            _eventTest = eventTest;
            _coordinateRepository = coordinateRepository;
        }

        [HttpPost]
        public async Task<ActionResult> AddCoordinate(Coordinate coordinateEntity)
        {
            _coordinateRepository.Add(coordinateEntity);
            await _coordinateRepository.SaveAsync();
            return Ok(new MsgReturn<object> {Msg = "添加坐标成功!",});
        }


        // 注册事件 
        [HttpGet("registerEvent/{userId}")]
        public async Task<ActionResult> RegisterEvent(Guid userId)
        {
            _eventTest.RegisterEvent();
            await _eventTest.Raise(userId);
            return Ok(new MsgReturn<object> {Msg = "注册事件成功!",});
        }


        // 注册热键 
        [Authorize]
        [HttpGet("logoutEvent")]
        public async Task<ActionResult> LogoutEvent()
        {
            await _eventTest.LogoutEvent();
            return Ok(new MsgReturn<object> {Msg = "注销热键成功!",});
        }
    }
}