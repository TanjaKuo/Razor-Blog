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

        public async Task OnGet(Guid id)
        //public void OnGet(Guid id)
        {
            // by using BlogPost, it will ensure we have id, thus we don't need to check if id == null 
            BlogPost = await _bloggieDbContext.BlogPosts.FindAsync(id);
        }

        // because we have two btns in edit page, using OnPostEdit instead of OnPost
        //public IActionResult OnPostEdit()
        public async Task<IActionResult> OnPostEdit()
        {
            // var existingBlogPost = _bloggieDbContext.BlogPosts.Find(BlogPost.Id);
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(BlogPost.Id);

            if (existingBlogPost != null)
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
            //_bloggieDbContext.SaveChanges();
            await _bloggieDbContext.SaveChangesAsync();

            return RedirectToPage("/Admin/Blogs/List");
        }

        // if we don't use OnPost keyword, we have to use [HttpPost] att
        //public IActionResult OnPostDelete()
        public async Task<IActionResult> OnPostDelete()
        {
            //var existingBlogPost = _bloggieDbContext.BlogPosts.Find(BlogPost.Id);
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(BlogPost.Id);

            if (existingBlogPost != null)
            {
                _bloggieDbContext.BlogPosts.Remove(existingBlogPost);
                //_bloggieDbContext.SaveChanges();
                await _bloggieDbContext.SaveChangesAsync();

                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }
    }
}
