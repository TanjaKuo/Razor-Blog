using System;
using BloggieWeb.Data;
using BloggieWeb.Models.Domain;

namespace BloggieWeb.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {

        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await _bloggieDbContext.BlogPostComment.AddAsync(blogPostComment);
            await _bloggieDbContext.SaveChangesAsync();

            return blogPostComment;
        }
    }
}

