using System;
using Web.Providers.Entities;

namespace Web.Providers.Contracts
{
	public interface IRoomProvider
	{
        Task<IEnumerable<Room>> GetRoomNames();
        Task<Room> GetRoomSpecs(int roomId);
    }
}

