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

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) { return Page(); }

            //Verifycredentials
            if (_auth.CheckCredentials(Credentials))
            {
                //creating the security context

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@pum.com"),
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return Redirect("/Index");
            }

            return Page();
        }
    }
}
