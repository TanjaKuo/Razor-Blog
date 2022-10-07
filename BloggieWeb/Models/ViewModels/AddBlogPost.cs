using System;
using System.ComponentModel.DataAnnotations;

namespace BloggieWeb.Models.ViewModels
{
    public class AddBlogPost
    {
        [Required]
        public string Heading { get; set; }

        [Required]
        public string PageTitle { get; set; }

        [Required]
        [MinLength(100)]
        public string Content { get; set; }

        [Required]
        [MinLength(10)]
        public string ShortDescription { get; set; }

        [Required]
        [Url]
        public string FeaturedImageUrl { get; set; }

        [Required]
        public string UrlHandle { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        public string Author { get; set; }

        public bool Visible { get; set; }
    }
}

