using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Services.Base;

namespace ChatRoom.Services.Services.Contracts
{
	public interface IUserService : IService
	{
        User GetUserById(int id);
        User CreateUser(User id);
    }
}

