using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;
using ChatRoom.Services.Services.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Api.ChatHub
{
	public class ChatHub : Hub
	{
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
		{
            _messageService = messageService;
		}

		public async Task SendMessage(string roomId, string username, string message)
		{
			int.TryParse(roomId, out int roomIdInt);

			var messageDto = new MessageDTO
			{
				MessagePrompt = message,
				RoomId = roomIdInt,
				PostingTime = DateTime.Now,
				Username = username
			};

            var result = _messageService.CreateMessage(roomIdInt, messageDto);

			await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", result);
        }

		public async Task SendStock()
		{

		}

		public async Task AddToGroup(string roomId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            await Clients.Group(roomId).SendAsync("ShowWho",
				$"Somebody connected {Context.ConnectionId}");
        }
    }
}

