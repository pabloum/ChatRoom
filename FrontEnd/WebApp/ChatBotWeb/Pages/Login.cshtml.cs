using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Security;

namespace ChatBotWeb.Pages
{
	public class LoginModel : PageModel
    {
        private readonly IAuthentication _auth;

        public LoginModel(IAuthentication auth)
        {
            _auth = auth;
        }

        [BindProperty]
        public Credentials Credentials { get; set; }

        public IActionResult OnGet()
        {
            if (base.User.Identity.IsAuthenticated)
            {
                return Redirect("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (base.User.Identity.IsAuthenticated) { return Redirect("/Index"); }

            if (!ModelState.IsValid) { return Page(); }

            if (await _auth.CheckCredentials(Credentials))
            {
                await _auth.LoginUser(Credentials);
                return Redirect("/Index");
            }

            return Page();
        }
    }
}
