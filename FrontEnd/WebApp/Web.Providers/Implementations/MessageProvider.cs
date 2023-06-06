using System;
using System.Text.Json;
using Web.Providers.Contracts;
using Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Web.Providers.Implementations
{
	public class MessageProvider : IMessageProvider
	{
        private IServiceHandler _serviceHandler;
        private IHttpContextAccessor _httpContextAccessor;

        public MessageProvider(IServiceHandler serviceHandler, IHttpContextAccessor httpContextAccessor)
		{
            _serviceHandler = serviceHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Message> CreateMessage(string messagePrompt, Room room)
        {
            var message = new Message();
            message.MessagePrompt = messagePrompt;
            message.PostingTime = DateTime.Now;
            message.RoomId = room.RoomId;
            message.Room = room;

            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null && httpContext.User.Identity.IsAuthenticated)
            {
                string username = httpContext.User.FindFirst("Username").Value;

                message.UserId = 1;
                message.User = new User //todo: take this from cookie
                {
                    UserId = 1,
                    Name = "Pablo Uribe",
                    Username = username,
                    Password = "123"
                };
            }

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

