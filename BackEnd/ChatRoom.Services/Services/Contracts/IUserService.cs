using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Services.Base;

namespace ChatRoom.Services.Services.Contracts
{
	public interface IUserService : IService
	{
        User GetUserByUsername(string username);
        bool CheckCredentials(User credentials);
        User CreateUser(User id);
    }
}

