using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public List<BlogPost> BlogPosts { get; set; }

        public ListModel(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }


        //public void OnGet()
        public async Task OnGet()
        {
          BlogPosts = await _bloggieDbContext.BlogPosts.ToListAsync();
        }
    }
}
