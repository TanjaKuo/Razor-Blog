//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BloggieWeb.Models.ViewModels;
//using BloggieWeb.Repositories;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace BloggieWeb.Pages.Admin.Users
//{
//    [Authorize(Roles = "Admin")]
//    public class IndexModel : PageModel
//    {
//        private readonly IUserRepository _userRepository;

//        // we don't want to show all the user identity, only a few, sp we create a viewmodel
//        public List<User> Users { get; set; }

//        [BindProperty]
//        public AddUser AddUserRequest { get; set; }

//        [BindProperty]
//        public Guid SelectedUserId { get; set; }

//        public IndexModel(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }


//        public async Task<IActionResult> OnGet()
//        {
//            var users = await _userRepository.GetAll();

//            Users = new List<User>();
//            foreach (var user in users)
//            {
//                Users.Add(new Models.ViewModels.User()
//                {
//                    Id = Guid.Parse(user.Id),
//                    Username = user.UserName,
//                    Email = user.Email
//                });
//            }
//            return Page();
//        }

//        public async Task<IActionResult> OnPost()
//        {
//            var identityUser = new IdentityUser
//            {
//                UserName = AddUserRequest.Username,
//                Email = AddUserRequest.Email
//            };

//            var roles = new List<string> { "User" };
//            if (AddUserRequest.AdminCheckbox)
//            {
//                roles.Add("Admin");
//            }

//            var result = await _userRepository.Add(identityUser, AddUserRequest.Password, roles);

//            if (result)
//            {
//                return RedirectToPage("/Admin/Users/Index");
//            };
//            return Page();
//        }


//        public async Task<IActionResult> OnPostDelete()
//        {
//            await _userRepository.Delete(SelectedUserId);

//            return RedirectToPage("/Admin/Users/Index");
//        }
//    }
//}

using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public List<User> Users { get; set; }

        [BindProperty]
        public AddUser AddUserRequest { get; set; }

        [BindProperty]
        public Guid SelectedUserId { get; set; }

        public IndexModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> OnGet()

            {
            var users = await _userRepository.GetAll();

            Users = new List<User>();
            foreach (var user in users)
            {
                Users.Add(new Models.ViewModels.User()
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email
                });
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
           
                var identityUser = new IdentityUser
                {
                    UserName = AddUserRequest.Username,
                    Email = AddUserRequest.Email
                };

                var roles = new List<string> { "User" };

                if (AddUserRequest.AdminCheckbox)
                {
                    roles.Add("Admin");
                }

                var result = await _userRepository.Add(identityUser, AddUserRequest.Password, roles);

                if (result)
                {
                    return RedirectToPage("/Admin/Users/Index");
                }

                return Page();
       
        }


        public async Task<IActionResult> OnPostDelete()
        {
            await _userRepository.Delete(SelectedUserId);
            return RedirectToPage("/Admin/Users/Index");
        }


        

    }
}