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
        public Credentials Credentials { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _auth.RegisterNewUser(Credentials);

            return Redirect("/Login");
        }
    }
}
