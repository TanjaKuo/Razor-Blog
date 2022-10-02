using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        //private readonly BloggieDbContext _bloggieDbContext;
        private readonly IBlogPostRepository _blogPostRepository;

        [BindProperty]
        public BlogPost BlogPost { get; set; }

        //public EditModel(BloggieDbContext bloggieDbContext)
        public EditModel(IBlogPostRepository blogPostRepository)
        {
            this._blogPostRepository = blogPostRepository;
        }

        public async Task OnGet(Guid id)
        //public void OnGet(Guid id)
        {
            // by using BlogPost, it will ensure we have id, thus we don't need to check if id == null 
            //BlogPost = await _bloggieDbContext.BlogPosts.FindAsync(id);
            BlogPost = await _blogPostRepository.GetAsync(id);
        }

        // because we have two btns in edit page, using OnPostEdit instead of OnPost
        //public IActionResult OnPostEdit()
        public async Task<IActionResult> OnPostEdit()
        {
            // var existingBlogPost = _bloggieDbContext.BlogPosts.Find(BlogPost.Id);


            // dbcontext and EF are looking after our code, so we don't need to update it just need to save it
            //_bloggieDbContext.BlogPosts.Update(existingBlogPost);
            //_bloggieDbContext.SaveChanges();

            await _blogPostRepository.UpdateAsync(BlogPost);
            ViewData["MessageDescription"] = "Record was successfully saved!";

            //return RedirectToPage("/Admin/Blogs/List");
            return Page();
        }

        // if we don't use OnPost keyword, we have to use [HttpPost] att
        //public IActionResult OnPostDelete()
        public async Task<IActionResult> OnPostDelete()
        {
            //var existingBlogPost = _bloggieDbContext.BlogPosts.Find(BlogPost.Id);

            var deleted = await _blogPostRepository.DeleteAsync(BlogPost.Id);
            if(deleted)
            {
                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }
    }
}
