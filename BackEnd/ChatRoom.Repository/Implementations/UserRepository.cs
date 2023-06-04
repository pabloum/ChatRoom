using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Persistence.Context;
using ChatRoom.Persistence.InMemoryData;
using ChatRoom.Repository.Base;
using ChatRoom.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChatRoom.Repository.Implementations
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(ChatRoomDbContext context, IInMemoryData inMemoryData, IConfiguration configuration)
            : base(context, inMemoryData, configuration)
        {
		}

        public User CreateUser(User user)
        {
            return _useDataBase ?
                    DbSet.Add(user).Entity
                    : _inMemoryData.CreateUser(user);
        }

        public User GetUserById(int userId)
        {
            return _useDataBase ?
                    DbSet.AsNoTracking().FirstOrDefault(u => u.UserId == userId)
                : _inMemoryData.GetUserById(userId);
        }
    }
}

