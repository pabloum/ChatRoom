using System;

namespace Entities
{
	public class Message
	{
        public int MessageId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string? MessagePrompt { get; set; }
        public DateTime PostingTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}

