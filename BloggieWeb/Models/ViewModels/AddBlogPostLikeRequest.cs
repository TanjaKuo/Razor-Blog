﻿using System;
namespace BloggieWeb.Models.ViewModels
{
    public class AddBlogPostLikeRequest
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}

