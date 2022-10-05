using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BloggieWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostLikeController : Controller
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
        }

        [Route("Add")]
        [HttpPost]
        // GET: /<controller>/
        public async Task<IActionResult> AddLike([FromBody] AddBlogPostLikeRequest addBlogPostLikeRequest)
        {
            await _blogPostLikeRepository.AddLikeForBlog(addBlogPostLikeRequest.BlogPostId,
                addBlogPostLikeRequest.UserId);

            return Ok();
        }

        [Route("{blogPostId:Guid}/totalLikes")]
        [HttpGet]
        public async Task<IActionResult> GetTotalLike([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _blogPostLikeRepository.GetTotalLikeForBlog(blogPostId);

            return Ok(totalLikes);
        }
    }
}

