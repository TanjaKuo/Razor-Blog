using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Models.Domain;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public List<BlogPost> Blogs { get; set; }

        public DetailsModel(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        public async Task<IActionResult> OnGet(string tagName)
        {
            Blogs = (await _blogPostRepository.GetAllAsync(tagName)).ToList();
            return Page();
        }
    }
}
