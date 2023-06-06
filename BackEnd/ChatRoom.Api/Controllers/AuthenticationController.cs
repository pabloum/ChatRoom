using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ChatRoom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private string SecurityKey;

		public AuthenticationController(IConfiguration configuration)
		{
            SecurityKey = configuration.GetSection("SecurityKey").Value;
        }

        [HttpPost]
        public IActionResult Index([FromBody]Credentials credentials)
        {
            if (credentials.UserName == "puribe" && credentials.Password == "123") //TODO: Create a proper validation
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

    public class Credentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

