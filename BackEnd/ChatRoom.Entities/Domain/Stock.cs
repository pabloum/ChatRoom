﻿using System;
namespace ChatRoom.Entities.Domain
{
	public class Stock
	{
		public string Code { get; set; }
		public string DateTime { get; set; }
		public string Open { get; set; }
		public string High { get; set; }
		public string Low { get; set; }
		public string Close { get; set; }
		public string Volume { get; set; }
	}
}

