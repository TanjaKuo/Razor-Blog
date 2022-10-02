using System;
namespace BloggieWeb.Repositories
{
    public interface IImageRepository
    {
        Task<string> UpdateAsync(IFormFile file);
    }
}

