using System;
using System.Collections.Generic;
using BloggieWeb.Models.Domain;

namespace BloggieWeb.Repositories
{
    public interface IBlogPostLikeRepository
    {

        Task<int> GetTotalLikeForBlog(Guid blogPostId);

        Task AddLikeForBlog(Guid blogPostId, Guid userId);

        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
    }
}

