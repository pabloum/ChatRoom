using System;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Api.ChatHub
{
	public class HubImplementation : Hub
	{
		public async Task SendMessage(string room, string user, string message)
		{
			await Clients.Group(room).SendAsync("Receive message", user, message);
		}

		public async Task AddToGroup(string room)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, room);

            await Clients.Group(room).SendAsync("ShowWho",
				$"Somebody connected {Context.ConnectionId}");
        }
    }
}

