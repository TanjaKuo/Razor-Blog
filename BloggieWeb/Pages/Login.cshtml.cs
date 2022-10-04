using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages
{
    public class LoginModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManger;

        [BindProperty]
        public Login LoginViewModel { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManger)
        {
            _signInManger = signInManger;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var signInResult = await _signInManger.PasswordSignInAsync(
                LoginViewModel.Username, LoginViewModel.Password, false, false);

            if(signInResult.Succeeded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Notification"] = new Notification
                {
                    Type = Enums.NotificationType.Error,
                    Message = "Unable to login"
                };
                return Page();
            }    
        }


    }
}
