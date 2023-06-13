using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Providers.Contracts;
using Entities;
using Web.Providers.Implementations;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace ChatBotWeb.Pages
{
    [Authorize]
	public class ChatRoomModel : PageModel
    {
        private readonly IMessageProvider _messageProvider;
        private readonly IRoomProvider _roomProvider;
        private readonly IStockProvider _stockProvider;
        private readonly IUserProvider _userProvider;

        public IEnumerable<Message> Messages { get; set; }

        public Room Room { get; set; }
        public User LoggedUser { get; set; }

        public ChatRoomModel(IMessageProvider messageProvider, IRoomProvider roomProvider, IStockProvider stockProvider, IUserProvider userProvider)
        {
            _messageProvider = messageProvider;
            _roomProvider = roomProvider;
            _stockProvider = stockProvider;
            _userProvider = userProvider;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);
            Room = await _roomProvider.GetRoomSpecs(id.Value);
            LoggedUser = await _userProvider.GetUserByUsername(User.FindFirst("Username").Value);

            return Page();
        }

        [BindProperty]
        public string NewMessage { get; set; }

        public async Task<IActionResult> OnPostMessageAsync(int? id)
        {
            Room = await _roomProvider.GetRoomSpecs(id.Value);

            if (!String.IsNullOrEmpty(NewMessage))
            {
                await _messageProvider.CreateMessage(NewMessage, Room);
            }

            Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);
            return Page();
        }

        [BindProperty]
        public string StockQuote { get; set; }

        public async Task<IActionResult> OnPostStockAsync(int? id)
        {
            Room = await _roomProvider.GetRoomSpecs(id.Value);
            await _stockProvider.GetStockQuote(id.Value, StockQuote);
            Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);

            return Page();
        }
    }
}
