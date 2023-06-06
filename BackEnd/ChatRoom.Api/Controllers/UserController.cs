using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;
using ChatRoom.Services.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
	{
        private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
            _userService = userService;

        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(string username)
        {
            var result = _userService.GetUserByUsername(username);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            var result = _userService.CreateUser(user);
            return Ok(result);
        }

        [HttpPost("authentication/checkCredentials")]
        public ActionResult<bool> CheckCredentials([FromBody] User user)
        {
            var result = _userService.CheckCredentials(user);
            return Ok(result);
        }
    }
}

