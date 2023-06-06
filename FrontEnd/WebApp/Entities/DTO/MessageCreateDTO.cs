using System;
namespace Entities.DTO
{
	public class MessageCreateDTO
	{
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string? MessagePrompt { get; set; }
        public DateTime PostingTime { get; set; }
        public string Username { get; set; }
    }
}

