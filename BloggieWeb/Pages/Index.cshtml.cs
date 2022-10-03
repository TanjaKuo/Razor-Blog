using BloggieWeb.Models.Domain;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ITagRepository _tagRepository;

    // we don't want any data coming from index, so we skip the BindProperty
    public List<BlogPost> Blogs { get; set; }
    public List<Tag> Tags { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
    {
        _logger = logger;
        _blogPostRepository = blogPostRepository;
        _tagRepository = tagRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        Blogs = (await _blogPostRepository.GetAllAsync()).ToList();
        Tags = (await _tagRepository.GetAllAsync()).ToList();
        return Page();
    }
}

