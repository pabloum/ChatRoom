using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;
using ChatRoom.Entities.Mappers;
using ChatRoom.Repository.Contracts;
using ChatRoom.Services.Services.Contracts;

namespace ChatRoom.Services.Services.Implementations
{
	public class MessageService : IMessageService
	{
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
		{
            _messageRepository = messageRepository;
        }

        public Message CreateMessage(int roomId, MessageDTO message)
        {

            return _messageRepository.CreateMessage(roomId, message.MapToMessage());
        }

        public IEnumerable<Message> GetMessagesByRoom(int roomId)
        {
            return _messageRepository.GetMessagesByRoom(roomId);
        }
    }
}

