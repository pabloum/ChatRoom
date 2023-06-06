using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Security;

namespace ChatBotWeb.Pages
{
	public class SignupModel : PageModel
    {
        private readonly IAuthentication _auth;

        public SignupModel(IAuthentication auth)
        {
            _auth = auth;
        }

        [BindProperty]
        public User NewUser { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _auth.RegisterNewUser(NewUser);

            return Redirect("/Login");
        }
    }
}
