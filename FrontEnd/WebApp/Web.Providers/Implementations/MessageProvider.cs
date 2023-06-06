using System;
using System.Text.Json;
using Web.Providers.Contracts;
using Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Entities.DTO;

namespace Web.Providers.Implementations
{
	public class MessageProvider : IMessageProvider
	{
        private IServiceHandler _serviceHandler;
        private IHttpContextProvider _httpContextProvider;

        public MessageProvider(IServiceHandler serviceHandler, IHttpContextProvider httpContextProvider)
		{
            _serviceHandler = serviceHandler;
            _httpContextProvider = httpContextProvider;
        }

        public async Task<Message> CreateMessage(string messagePrompt, Room room)
        {
            var message = new MessageCreateDTO()
            {
                MessagePrompt = messagePrompt,
                PostingTime = DateTime.Now,
                RoomId = room.RoomId,
                Username = _httpContextProvider.GetClaim("Username"),
            };

            var response = await _serviceHandler.Post<Message>($"api/Message/{room.RoomId}", JsonSerializer.Serialize(message));
            return response;
        }

        public async Task<IEnumerable<Message>> GetAllMessagedByRoom(int roomId)
        {
            var response = await _serviceHandler.Get<IEnumerable<Message>>($"api/Message/{roomId}");
            return response != null ? response : new List<Message>();
        }
    }
}

