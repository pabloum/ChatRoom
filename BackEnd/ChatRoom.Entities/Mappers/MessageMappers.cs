using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;

namespace ChatRoom.Entities.Mappers
{
	public static class MessageMappers
	{
        public static Message MapToMessage(this MessageDTO messageDTO)
        {
            return new Message
            {
                RoomId = messageDTO.RoomId,
                Room = new Room { RoomId = messageDTO.RoomId, RoomName = "" },
                MessagePrompt = messageDTO.MessagePrompt,
                PostingTime = messageDTO.PostingTime,
                UserId = 1,
                User = new User
                {
                    UserId = 1, Name = "", Username = messageDTO.Username, Password = String.Empty
                }
            };
        }
    }
}