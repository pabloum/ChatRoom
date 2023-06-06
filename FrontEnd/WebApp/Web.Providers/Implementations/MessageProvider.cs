using System;
using System.Text.Json;
using Web.Providers.Contracts;
using Entities;

namespace Web.Providers.Implementations
{
	public class MessageProvider : IMessageProvider
	{
        private IServiceHandler _serviceHandler;

        public MessageProvider(IServiceHandler serviceHandler)
		{
            _serviceHandler = serviceHandler;
        }

        public async Task<Message> CreateMessage(string messagePrompt, Room room)
        {
            var message = new Message();
            message.MessagePrompt = messagePrompt;
            message.PostingTime = DateTime.Now;
            message.RoomId = room.RoomId;
            message.Room = room;
            message.UserId = 1;
            message.User = new User
            {
                UserId = 1,
                Name = "Pablo Uribe",
                Username = "puribe"
            };

            var response = await _serviceHandler.Post<Message>($"api/Message/{room.RoomId}", JsonSerializer.Serialize(message));
            //if (response == null) throw new Exception("Something went wrong");
            return response;
        }

        public async Task<IEnumerable<Message>> GetAllMessagedByRoom(int roomId)
        {
            var response = await _serviceHandler.Get<IEnumerable<Message>>($"api/Message/{roomId}");
            return response != null ? response : new List<Message>();
        }
    }
}

