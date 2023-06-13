using System;
using ChatRoom.Entities.Domain;

namespace ChatRoom.Entities.DTO
{
	public class MessageDTO
	{
        public int RoomId { get; set; }
        public string? MessagePrompt { get; set; }
        public DateTime PostingTime { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
    }
}

