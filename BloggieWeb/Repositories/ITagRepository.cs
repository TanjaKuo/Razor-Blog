using System;
using BloggieWeb.Models.Domain;

namespace BloggieWeb.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}

