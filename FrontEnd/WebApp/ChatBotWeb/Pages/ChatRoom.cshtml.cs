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

        [BindProperty]
        public IEnumerable<Message> Messages { get; set; }

        [BindProperty]
        public Room Room { get; set; }

        public ChatRoomModel()
        {
            _messageProvider = new MessageProvider();
            _roomProvider = new RoomProvider();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);
            Room = await _roomProvider.GetRoomSpecs(id.Value);

            if(Room == null)
            {
                return NotFound();
            }

            return Page();
        }

        [BindProperty]
        public string NewMessage { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //if (!ModelState.IsValid)
            //{
            //    Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);
            //    Room = await _roomProvider.GetRoomSpecs(id.Value);
            //    return Page();
            //}

            Room = await _roomProvider.GetRoomSpecs(id.Value);
            await _messageProvider.CreateMessage(NewMessage, Room);


            Messages = await _messageProvider.GetAllMessagedByRoom(id.Value);
            return Page();
        }
    }
}
