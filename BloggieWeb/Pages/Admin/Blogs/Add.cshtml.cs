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
    public class AddModel : PageModel
    {

        //private readonly BloggieDbContext _bloggieDbContext;
        private readonly IBlogPostRepository _blogPostRepository;
        //        // options 2. set asp-for and bind propert 1 by 1
        //        //[BindProperty]
        //        //public string Heading { get; set; }

        //        // option 3. create model except id and bind together, asp-for="AddBlogPostRequest.propertyName"
        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }

        // now we talk to repository instead of DbContext
        //public AddModel(BloggieDbContext bloggieDbContext)
        public AddModel(IBlogPostRepository blogPostRepository)
        {
            //_bloggieDbContext = bloggieDbContext;
            _blogPostRepository = blogPostRepository;
        }

        public void OnGet()
        {
        }

        // in order to return something, changed void -> IActionResult

        public async Task<IActionResult> OnPost()
        //public IActionResult OnPost()
        {
            //            // option 1. set name on html and bind 1 by 1
            //            //var heading = Request.Form["heading"];
            //            //var pageTitle = Request.Form["pageTitle"];
            //            //var featuredImageUrl = Request.Form["featuredImageUrl"];


            var blogPost = new BlogPost()
            {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                Content = AddBlogPostRequest.Content,
                ShortDescription = AddBlogPostRequest.ShortDescription,
                FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                UrlHandle = AddBlogPostRequest.UrlHandle,
                PublishedDate = AddBlogPostRequest.PublishedDate,
                Author = AddBlogPostRequest.Author,
                Visible = AddBlogPostRequest.Visible

            };

            await _blogPostRepository.AddAsync(blogPost);
            //_bloggieDbContext.BlogPosts.Add(blogPost);
            //_bloggieDbContext.SaveChanges();

            var notification = new Notification
            {
                Message = "New blog post created successfully!",
                Type = Enums.NotificationType.Success
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage("/Admin/Blogs/List");
        }
    }
}
