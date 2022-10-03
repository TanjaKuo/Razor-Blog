using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
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
        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        [BindProperty]
        public string Tags { get; set; }

        //public EditModel(BloggieDbContext bloggieDbContext)
        public EditModel(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task OnGet(Guid id)
        //public void OnGet(Guid id)
        {
            // by using BlogPost, it will ensure we have id, thus we don't need to check if id == null 
            //BlogPost = await _bloggieDbContext.BlogPosts.FindAsync(id);
            BlogPost = await _blogPostRepository.GetAsync(id);

            if(BlogPost != null && BlogPost.Tags != null)
            {
                Tags = string.Join(',', BlogPost.Tags.Select(x => x.Name));
            }
        }

        // because we have two btns in edit page, using OnPostEdit instead of OnPost
        //public IActionResult OnPostEdit()
        public async Task<IActionResult> OnPostEdit()
        {
            // var existingBlogPost = _bloggieDbContext.BlogPosts.Find(BlogPost.Id);


            // dbcontext and EF are looking after our code, so we don't need to update it just need to save it
            //_bloggieDbContext.BlogPosts.Update(existingBlogPost);
            //_bloggieDbContext.SaveChanges();

           try
                
        {
                BlogPost.Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag { Name = x.Trim() }));
 
                await _blogPostRepository.UpdateAsync(BlogPost);
                //ViewData["MessageDescription"] = "Record was successfully saved!";

                ViewData["Notification"] = new Notification
                {
                    Message = "Record update successfully!",
                    Type = Enums.NotificationType.Success
                };
            }catch(Exception e)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Oh no! Something went wrong",
                    Type = Enums.NotificationType.Error
                };
            }

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
                var notification = new Notification
                {
                    Message = "Blog post was deleted successfully!",
                    Type = Enums.NotificationType.Success
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }
    }
}
