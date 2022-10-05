using System;
using System.Collections;
using BloggieWeb.Models.Domain;

namespace BloggieWeb.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId);


    }
}

//using BloggieWeb.Models.Domain;

//namespace BloggieWeb.Repositories
//{
//    public interface IBlogPostCommentRepository
//    {
//        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

//        Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId);
//    }
//}