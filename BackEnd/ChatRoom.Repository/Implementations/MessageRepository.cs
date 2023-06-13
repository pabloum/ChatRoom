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
	public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
		public MessageRepository(ChatRoomDbContext context, IInMemoryData inMemoryData, IConfiguration configuration)
            : base(context, inMemoryData, configuration)
        {
		}

        public Message CreateMessage(int roomId, Message message)
        {
            if (_useDataBase)
            {
                message.Room = null; // TODO: improve this
                message.User = null;

                var added = DbSet.Add(message);
                _context.SaveChanges();
                return added.Entity;
            }
            else
            {
                return _inMemoryData.CreateMessage(message);
            }
        }

        public IEnumerable<Message> GetMessagesByRoom(int roomId)
        {
            var result = _useDataBase ?
                    DbSet.Include(m => m.User).Include(m => m.Room).AsNoTracking().Where(m => m.RoomId == roomId)
                : _inMemoryData.GetMessegesByRoom(roomId);

            return result.Take(50).OrderBy(m => m.PostingTime);
        }
    }
}

