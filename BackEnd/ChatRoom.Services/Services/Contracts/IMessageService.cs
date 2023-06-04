using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Services.Base;

namespace ChatRoom.Services.Services.Contracts
{
	public interface IMessageService : IService
	{
        IEnumerable<Message> GetMessagesByRoom(int roomId);
        Message CreateMessage(int roomId, Message message);
    }
}

