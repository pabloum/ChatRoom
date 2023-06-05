using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Providers.Contracts;
using Web.Providers.Entities;
using Web.Providers.Implementations;

namespace ChatBotWeb.Pages
{
	public class ChatRoomModel : PageModel
    {
        private readonly IMessageProvider _messageProvider;
        private readonly IRoomProvider _roomProvider;
        private readonly IStockProvider _stockProvider;

        public int Id { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public Room Room { get; set; }

        public ChatRoomModel()
        {
            _messageProvider = new MessageProvider();
            _roomProvider = new RoomProvider();
            _stockProvider = new StockProvider();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Id = id.Value;
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
            Id = id.Value;
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
            Id = id.Value;
            Room = await _roomProvider.GetRoomSpecs(id.Value);
            Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);

            var quote = await _stockProvider.GetStockQuote();

            if (!String.IsNullOrEmpty(quote))
            {
                Messages = Messages.Append(new Message
                {
                    MessagePrompt = quote,
                    PostingTime = DateTime.Now,
                    User = new User { Name = "The Bot", Username = "bot" }
                });
            }

            return Page();
        }
    }
}
