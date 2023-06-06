using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Repository.Base;

namespace ChatRoom.Repository.Contracts
{
	public interface IUserRepository : IRepository
	{
		User GetUserByUsername(string username);
		User CreateUser(User user);
	}
}

