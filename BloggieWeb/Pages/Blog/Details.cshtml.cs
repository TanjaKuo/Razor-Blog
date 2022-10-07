using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
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

        public List<BlogComment> Comments { get; set; }

        public int TotalLike { get; set; }
        public bool Liked { get; set; }

        // bind with UI, get id from OnGet, comment
        [BindProperty]
        public Guid BlogPostId { get; set; }

        [BindProperty]
        [Required]
        [MinLength(2), MaxLength(200)]
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
            await GetBlog(urlHandle);

            return Page();
        }

        public async Task<IActionResult> OnPost(string urlHandle)
        {

            if(ModelState.IsValid)
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

            await GetBlog(urlHandle);

            return Page();
        }

        private async Task GetComments()
        {
            // get all comments
            var blogPostComments = await _blogPostCommentRepository.GetAllAsync(BlogPost.Id);

            var blogPostCommentViewModel = new List<BlogComment>();

            foreach (var blogPostComment in blogPostComments)
            {
                blogPostCommentViewModel.Add(new BlogComment
                {
                    DateAdded = blogPostComment.DateAdded,
                    Description = blogPostComment.Description,
                    Username = (await _userManager.FindByIdAsync(blogPostComment.UserId.ToString())).UserName
                });
            }

            Comments = blogPostCommentViewModel;

        }


        private async Task GetBlog(string urlHandle)
        {
            BlogPost = await _blogPostRepository.GetAsync(urlHandle);

            if (BlogPost != null)
            {

                BlogPostId = BlogPost.Id;
                // check if user login, by using  nad userManager
                if (_signInManager.IsSignedIn(User))
                {
                    var likes = await _blogPostLikeRepository.GetLikesForBlog(BlogPost.Id);
                    // check if user already like the post
                    var userId = _userManager.GetUserId(User);

                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));

                    await GetComments();


                }

                TotalLike = await _blogPostLikeRepository.GetTotalLikeForBlog(BlogPost.Id);
            }
        }

    }
}
