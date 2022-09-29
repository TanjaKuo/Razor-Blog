using System;
namespace BloggieWeb.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Guid BlogPostId { get; set; }
    }
}

