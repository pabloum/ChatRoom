using System;
using System.Text.Json;
using Entities;
using Web.Providers;

namespace Security
{
	public interface IAuthentication
	{
        Task<bool> CheckCredentials(Credentials credentials);
        Task RegisterNewUser(User credentials);
    }


    public class Authentication : IAuthentication
	{
        private readonly IServiceHandler _serviceHandler;

        public Authentication(IServiceHandler serviceHandler)
        {
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

        public async Task RegisterNewUser(User credentials)
        {
            var response = await _serviceHandler.Post<User>("api/User", JsonSerializer.Serialize(credentials));
        }
    }
}

