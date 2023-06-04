using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Repository.Contracts;
using ChatRoom.Services.Services.Contracts;

namespace ChatRoom.Services.Services.Implementations
{
	public class UserService : IUserService
	{
        private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
            _userRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
    }
}

