using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Repository.Base;

namespace ChatRoom.Repository.Contracts
{
	public interface IMessageRepository : IRepository
	{
		IEnumerable<Message> GetMessagesByRoom(int roomId);
		Message CreateMessage(int roomId, Message message);
	}
}

