using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        //private readonly BloggieDbContext _bloggieDbContext;
        private readonly IBlogPostRepository _blogPostRepository;
        public List<BlogPost> BlogPosts { get; set; }

        //public ListModel(BloggieDbContext bloggieDbContext)
        public ListModel(IBlogPostRepository blogPostRepository)
        {
            this._blogPostRepository = blogPostRepository;
        }


        //public void OnGet()
        public async Task OnGet()
        {

            var messages = (string)TempData["MessageDescription"];

            if(!string.IsNullOrWhiteSpace(messages))
            {
                ViewData["MessageDescription"] = messages;
            }

            BlogPosts = (await _blogPostRepository.GetAllAsync())?.ToList();
        }
    }
}
