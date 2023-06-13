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
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IRoomRepository roomRepository, IUserRepository userRepository)
		{
            _messageRepository = messageRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }

        public Message CreateMessage(int roomId, MessageDTO message)
        {
            var user = _userRepository.GetUserByUsername(message.Username);
            return _messageRepository.CreateMessage(roomId, message.MapToMessage(user));
        }

        public IEnumerable<Message> GetMessagesByRoom(int roomId)
        {
            return _messageRepository.GetMessagesByRoom(roomId);
        }
    }
}

