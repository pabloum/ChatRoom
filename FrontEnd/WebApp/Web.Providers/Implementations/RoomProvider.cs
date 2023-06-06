using System;
using System.Net.Http.Headers;
using Web.Providers.Contracts;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Entities;
using System.Text.Json;

namespace Web.Providers.Implementations
{
	public class RoomProvider : IRoomProvider
	{
        private IServiceHandler _serviceHandler;

		public RoomProvider(IServiceHandler serviceHandler)
		{
            _serviceHandler = serviceHandler;
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

        public async Task CreateNewRoom(string newRoomName)
        {
            var room = new Room { RoomId = 0, RoomName = newRoomName };
            var response = await _serviceHandler.Post<Room>($"api/Room", JsonSerializer.Serialize(room));
            //if (response == null) throw new Exception("Something went wrong");
        }
    }
}

