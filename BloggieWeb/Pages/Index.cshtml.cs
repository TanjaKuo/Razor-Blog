using BloggieWeb.Models.Domain;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BloggieWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBlogPostRepository _blogPostRepository;

    // we don't want any data coming from index, so we skip the BindProperty
    public List<BlogPost> Blogs { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IBlogPostRepository blogPostRepository)
    {
        _logger = logger;
        _blogPostRepository = blogPostRepository;
    }

    public async Task<IActionResult> OnGet()
    {
        Blogs = (await _blogPostRepository.GetAllAsync()).ToList();
        return Page();
    }
}

