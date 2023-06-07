using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;

namespace ChatRoom.Services.Mappers
{
	public static class MessageMappers
	{
        public static Message MapToMessage(this MessageDTO messageDTO, Room room, User user)
        {
            return new Message
            {
                RoomId = messageDTO.RoomId,
                Room = room,
                MessagePrompt = messageDTO.MessagePrompt,
                PostingTime = messageDTO.PostingTime,
                UserId = user.UserId,
                User = user
            };
        }
    }
}