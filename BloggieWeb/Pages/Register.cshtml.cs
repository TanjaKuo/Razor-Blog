﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManger;

        [BindProperty]
        public Register RegisterViewModel { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManger)
        {
            _userManger = userManger;
        }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPost()
        { 
            var user = new IdentityUser
            {
                UserName = RegisterViewModel.Username,
                Email = RegisterViewModel.Email,
            };

            var identityResult = await _userManger.CreateAsync(user, RegisterViewModel.Password);

            if(identityResult.Succeeded)
            {
                ViewData["Notification"] = new Notification
                {
                    Type = Enums.NotificationType.Success,
                    Message = "User registered successfully!"
                };
                return Page();
            }
                ViewData["Notification"] = new Notification
            {
                Type = Enums.NotificationType.Error,
                Message = "Something went wrong"
            };
                return Page();
        }
    }
}
