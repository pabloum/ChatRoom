using System;
using System.Security.Claims;
using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Formatting;
using Web.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Security;

namespace Security
{
    public class Authentication : IAuthentication
	{
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceHandler _serviceHandler;

        public Authentication(IHttpContextAccessor httpContextAccessor, IServiceHandler serviceHandler)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceHandler = serviceHandler;
        }

        public async Task<bool> CheckCredentials(Credentials credentials)
        {
            var user = new User
            {
                UserId = 0,
                Username = credentials.UserName,
                Name = String.Empty,
                Password = credentials.Password
            };
            var response = await _serviceHandler.Post<bool>($"api/User/authentication/checkCredentials", JsonSerializer.Serialize(user));
            return response;
        }

        public async Task LoginUser(Credentials credentials)
        {
            var claims = new List<Claim>
            {
                new Claim("Username", credentials.UserName),
                new Claim(ClaimTypes.Email, $"{credentials.UserName}@pum.com"),
                new Claim("Department", "Evaluator"),
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
        }

        public async Task RegisterNewUser(User credentials)
        {
            var response = await _serviceHandler.Post<User>("api/User", JsonSerializer.Serialize(credentials));
        }
    }
}

