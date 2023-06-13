﻿using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;

namespace ChatRoom.Entities.Mappers
{
	public static class MessageMappers
	{
        public static Message MapToMessage(this MessageDTO messageDTO, User user)
        {
            return new Message
            {
                RoomId = messageDTO.RoomId,
                Room = null,
                MessagePrompt = messageDTO.MessagePrompt,
                PostingTime = messageDTO.PostingTime,
                UserId = user.UserId,
                User = null
            };
        }
    }
}