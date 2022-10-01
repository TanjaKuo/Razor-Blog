﻿using System;
using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Repositories
{
    public class BlogPostRepository:IBlogPostRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _bloggieDbContext.BlogPosts.AddAsync(blogPost);
            await _bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _bloggieDbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
            return await _bloggieDbContext.BlogPosts.FindAsync(id);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(blogPost.Id);

            if (existingBlogPost != null)
            {

                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.Visible = blogPost.Visible;
            };

            await _bloggieDbContext.SaveChangesAsync();
            return existingBlogPost;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(id);

            if (existingBlogPost != null)
            {
                _bloggieDbContext.BlogPosts.Remove(existingBlogPost);
                await _bloggieDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        
    }
}

