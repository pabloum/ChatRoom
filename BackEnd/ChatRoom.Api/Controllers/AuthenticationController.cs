using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChatRoom.Entities.Domain;
using ChatRoom.Services.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ChatRoom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private string SecurityKey;

		public AuthenticationController(IConfiguration configuration, IUserService userService)
		{
            SecurityKey = configuration.GetSection("SecurityKey").Value;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Index([FromBody]User credentials)
        {
            if (_userService.CheckCredentials(credentials))
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "admin"),
                        new Claim(ClaimTypes.Email, "admin@pum.com"),
                        new Claim("Department", "Evaluator"),
                    };

                var expiresAt = DateTime.Now.AddHours(1);

                return Ok(new
                {
                    access_token = CreateToken(claims, expiresAt),
                    expires_at = expiresAt
                });
            }

            return Unauthorized();
        }

        private string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
        {
            var secretKey = Encoding.ASCII.GetBytes(SecurityKey);
            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                expires: expiresAt,
                signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(secretKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

