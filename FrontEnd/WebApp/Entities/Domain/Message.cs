﻿using System;

namespace Entities
{
	public class Message
	{
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string? MessagePrompt { get; set; }
        public DateTime PostingTime { get; set; }
        public string Username { get; set; }
    }
}

