using System;
using Entities;
using Web.Providers.Contracts;

namespace Web.Providers.Implementations
{
	public class UserProvider : IUserProvider
    {
        private IServiceHandler _serviceHandler;

        public UserProvider(IServiceHandler serviceHandler)
        {
            _serviceHandler = serviceHandler;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var response = await _serviceHandler.Get<User>($"api/User/{username}");
            return response;
        }
    }
}

