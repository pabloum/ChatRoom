using System;
namespace Web.Providers.Contracts
{
	public interface IRoomProvider
	{
        Task<IEnumerable<string>> GetRoomNames();
    }
}

