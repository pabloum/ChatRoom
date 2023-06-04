using System;
using System.Net.Http.Headers;
using Web.Providers.Contracts;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Providers.Entities;

namespace Web.Providers.Implementations
{
	public class RoomProvider : IRoomProvider
	{
        private ServiceHandler _serviceHandler;
		public RoomProvider()
		{
            _serviceHandler = new ServiceHandler(); // TODO: Use DI
        }

        public async Task<IEnumerable<Room>> GetRoomNames()
        {
            var response = await _serviceHandler.Get<IEnumerable<Room>>("api/Room/GetRooms");
            return response != null ? response : new List<Room>();
        }

        public async Task<Room> GetRoomSpecs(int roomId)
        {
            var response = await _serviceHandler.Get<Room>($"api/Room/{roomId}");
            return response;
        }
    }
}

