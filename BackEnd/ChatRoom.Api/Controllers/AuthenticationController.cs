using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.Mappers;
using ChatRoom.Security;
using ChatRoom.Services.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ChatRoom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _auth;

		public AuthenticationController(IConfiguration configuration, IAuthentication auth)
		{
            _auth = auth;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody]Credentials credentials)
        {
            var token = _auth.GetToken(credentials);

            if (token != null) { return Ok(token); }

            return Unauthorized();
        }
    }
}

