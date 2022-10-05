using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Models.Domain;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages.Blog
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBlogPostCommentRepository _blogPostCommentRepository;


        public BlogPost BlogPost { get; set; }
        public int TotalLike { get; set; }
        public bool Liked { get; set; }

        // bind with UI, get id from OnGet, comment
        [BindProperty]
        public Guid BlogPostId { get; set; }
        [BindProperty]
        public string CommentDescription { get; set; }

        public DetailsModel(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            _blogPostRepository = blogPostRepository;
            _blogPostLikeRepository = blogPostLikeRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _blogPostCommentRepository = blogPostCommentRepository;
        }

        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogPost = await _blogPostRepository.GetAsync(urlHandle);

            if(BlogPost != null)
            {
                BlogPostId = BlogPost.Id;
                // check if user login, by using  nad userManager
                if (_signInManager.IsSignedIn(User))
                {
                    var likes = await _blogPostLikeRepository.GetLikesForBlog(BlogPost.Id);
                    // check if user already like the post
                    var userId = _userManager.GetUserId(User);

                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
                }

                

                TotalLike = await _blogPostLikeRepository.GetTotalLikeForBlog(BlogPost.Id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(string urlHandle)
        {

            if (_signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(CommentDescription))
            {
                var userId = _userManager.GetUserId(User);

                var comment = new BlogPostComment()
                {
                    BlogPostId = BlogPostId,
                    Description = CommentDescription,
                    DateAdded = DateTime.Now,
                    UserId = Guid.Parse(userId)
                };
                await _blogPostCommentRepository.AddAsync(comment);
            }
            // go back to OnGet()
            return RedirectToPage("/Blog/Details", new { urlHandle = urlHandle });
        }
    }
}
