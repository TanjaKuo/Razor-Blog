using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Pages.Admin.Blogs
{
    [Authorize(Roles = "Admin")]

    public class ListModel : PageModel
    {
        //private readonly BloggieDbContext _bloggieDbContext;
        private readonly IBlogPostRepository _blogPostRepository;
        public List<BlogPost> BlogPosts { get; set; }

        //public ListModel(BloggieDbContext bloggieDbContext)
        public ListModel(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }


        //public void OnGet()
        public async Task OnGet()
        {


            var notificationJson = (string)TempData["Notification"];

                if(notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }
           
            BlogPosts = (await _blogPostRepository.GetAllAsync())?.ToList();
        }
    }
}
