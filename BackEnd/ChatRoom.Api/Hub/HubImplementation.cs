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
        private readonly IStockService _stockService;

        public ChatHub(IMessageService messageService, IStockService stockService)
		{
            _messageService = messageService;
            _stockService = stockService;
        }

		public async Task SendMessage(string roomId, string username, string message, string userId)
		{
			if (int.TryParse(roomId, out int roomIdInt) && int.TryParse(userId, out int userIdInt))
			{
				var messageDto = new MessageDTO
				{
					MessagePrompt = message,
					RoomId = roomIdInt,
					PostingTime = DateTime.Now,
					UserId = userIdInt,
                    Username = username
				};

				var newMessage = _messageService.CreateMessage(roomIdInt, messageDto);
				var result = _messageService.GetMessagesByRoom(roomIdInt);

				await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", result);
			}
        }

        public async Task GetStockQuote(string roomId, string stockCode)
        {
            if (int.TryParse(roomId, out int roomIdInt))
			{
				var stock = String.IsNullOrEmpty(stockCode) ? "aapl.us" : stockCode;

                var newMessage = await _stockService.GetStock(roomIdInt, stock);
				var result = _messageService.GetMessagesByRoom(roomIdInt);

				await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", result);
			}

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

