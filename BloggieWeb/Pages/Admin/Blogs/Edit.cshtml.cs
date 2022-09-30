using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly BloggieDbContext _bloggieDbContext;

        [BindProperty]
        public BlogPost BlogPost { get; set; }

        public EditModel(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }


        public void OnGet(Guid id)
        {
            // by using BlogPost, it will ensure we have id, thus we don't need to check if id == null 
            BlogPost = _bloggieDbContext.BlogPosts.Find(id);
        }

        public IActionResult OnPost()
        {
            var existingBlogPost = _bloggieDbContext.BlogPosts.Find(BlogPost.Id);

            if(existingBlogPost != null)
            {

                existingBlogPost.Heading = BlogPost.Heading;
                existingBlogPost.PageTitle = BlogPost.PageTitle;
                 existingBlogPost.Content = BlogPost.Content;
                 existingBlogPost.ShortDescription = BlogPost.ShortDescription;
                 existingBlogPost.FeaturedImageUrl = BlogPost.FeaturedImageUrl;
                 existingBlogPost.UrlHandle = BlogPost.UrlHandle;
                 existingBlogPost.PublishedDate = BlogPost.PublishedDate;
                 existingBlogPost.Author = BlogPost.Author;
                existingBlogPost.Visible = BlogPost.Visible;
            };


            // dbcontext and EF are looking after our code, so we don't need to update it just need to save it
            //_bloggieDbContext.BlogPosts.Update(existingBlogPost);
            _bloggieDbContext.SaveChanges();


            return RedirectToPage("/Admin/Blogs/List");
        }
    }
}
