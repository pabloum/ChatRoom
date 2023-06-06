using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Providers.Entities;

namespace ChatBotWeb.Pages
{
	public class SignupModel : PageModel
    {
        [BindProperty]
        public Credentials Credentials { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {

        }
    }
}
