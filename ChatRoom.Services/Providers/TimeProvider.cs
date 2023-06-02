using System;
using ChatRoom.Services.Providers.Contracts;

namespace ChatRoom.Services.Providers
{
	public class TimeProvider : ITimeProvider
	{
		public DateTime GetCurrentDateTime()
		{
			return DateTime.Now;
		}
	}
}

