using System;
using Entities;

namespace Web.Providers.Contracts
{
	public interface IUserProvider
	{
        Task<User> GetUserByUsername(string username);
    }
}

