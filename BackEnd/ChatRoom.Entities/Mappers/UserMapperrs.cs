using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;

namespace ChatRoom.Entities.Mappers
{
	public static class UserMapperrs
	{
        public static User MapCredentialsToUser(this Credentials credentials)
        {
            return new User
            {
                Username = credentials.Username,
                Password = credentials.Password
            };
        }
    }
}

