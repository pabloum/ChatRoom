using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Providers.Contracts;
using Entities;
using Web.Providers.Implementations;

namespace ChatBotWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IRoomProvider _roomProvider;

    public IEnumerable<Room> Rooms { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IRoomProvider roomProvider)
    {
        _logger = logger;
        _roomProvider = roomProvider;
    }

    public async Task OnGetAsync()
    {
        Rooms = await _roomProvider.GetRoomNames();
    }

    [BindProperty]
    public string NewRoomName { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Rooms = await _roomProvider.GetRoomNames();
            return Page();
        }

        await _roomProvider.CreateNewRoom(NewRoomName);
        Rooms = await _roomProvider.GetRoomNames();

        return RedirectToPage("./Index");
    }
}

