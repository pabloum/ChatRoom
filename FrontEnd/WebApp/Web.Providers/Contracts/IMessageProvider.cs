using System;
using Web.Providers.Entities;

namespace Web.Providers.Contracts
{
	public interface IMessageProvider
	{
		Task<IEnumerable<Message>> GetAllMessagedByRoom(int roomId);
		Task<Message> CreateMessage(Message message);
	}
}

