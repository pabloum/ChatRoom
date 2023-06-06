using System;
using System.Net;
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

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public bool CheckCredentials(User credentials)
        {
            var user = _userRepository.GetUserByUsername(credentials.Username);
            return user.Username == credentials.Username && user.Password == credentials.Password;
        }
    }
}

