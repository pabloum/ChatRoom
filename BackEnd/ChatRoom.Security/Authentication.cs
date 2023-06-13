using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.Mappers;
using ChatRoom.Services.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChatRoom.Security
{
    public class Authentication : IAuthentication
    {
        private readonly IUserService _userService;
        private readonly string SecurityKey;

        public Authentication(IConfiguration configuration, IUserService userService)
        {
            SecurityKey = configuration.GetSection("SecurityKey").Value;
            _userService = userService;
        }

        public object GetToken(Credentials credentials)
        {
            var user = _userService.GetUserByUsername(credentials.Username);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Username", user.Username),
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Email, $"{user.Username}@pum.com"),
                new Claim("Department", "Evaluator"),
            };

            var expiresAt = DateTime.Now.AddHours(1);

            return new
            {
                access_token = CreateToken(claims, expiresAt),
                expires_at = expiresAt
            };

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

