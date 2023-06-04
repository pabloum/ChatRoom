using System;
using ChatRoom.Services.Base;

namespace ChatRoom.Services.Providers.Contracts
{
	public interface IProvider
	{
	}

	public interface ITimeProvider : IProvider
	{
		DateTime GetCurrentDateTime();
	}
}

