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

namespace ChatBotWeb.Pages
{
    [Authorize]
	public class ChatRoomModel : PageModel
    {
        private readonly IMessageProvider _messageProvider;
        private readonly IRoomProvider _roomProvider;
        private readonly IStockProvider _stockProvider;

        public IEnumerable<Message> Messages { get; set; }

        public Room Room { get; set; }

        public ChatRoomModel(IMessageProvider messageProvider, IRoomProvider roomProvider, IStockProvider stockProvider)
        {
            _messageProvider = messageProvider;
            _roomProvider = roomProvider;
            _stockProvider = stockProvider;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);
            Room = await _roomProvider.GetRoomSpecs(id.Value);

            if (Room == null)
            {
                return NotFound();
            }

            return Page();
        }

        [BindProperty]
        public string NewMessage { get; set; }

        public async Task<IActionResult> OnPostMessageAsync(int? id)
        {
            Room = await _roomProvider.GetRoomSpecs(id.Value);

            if (!ModelState.IsValid)
            {
                Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);
                return Page();
            }

            await _messageProvider.CreateMessage(NewMessage, Room);
            Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);

            ModelState.Clear();

            return Page();
        }

        //[BindProperty]
        public string StockQuote { get; set; }

        public async Task<IActionResult> OnPostStockAsync(int? id)
        {
            Room = await _roomProvider.GetRoomSpecs(id.Value);
            var quote = await _stockProvider.GetStockQuote(id.Value/*, StockQuote*/);
            Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);

            return Page();
        }
    }
}
