using System;
using System.Collections;
using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;


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

    
        public async Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId)
        {
            return await _bloggieDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId)
               .ToListAsync();
        }
    }
}
