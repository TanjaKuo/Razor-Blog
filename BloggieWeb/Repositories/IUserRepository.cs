using System;
using Microsoft.AspNetCore.Identity;

namespace BloggieWeb.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();

        Task<bool> Add(IdentityUser identityUser, string password, List<string> roles);

    }
}

