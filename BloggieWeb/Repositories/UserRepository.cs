using System;
using BloggieWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AuthDbContext _authDbContext;
        public UserRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await _authDbContext.Users.ToListAsync();
            // not shoe super admin
            var superAdminUser = await _authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");

            // remove superadmin from users
            if(superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }

            return users;
        }
    }
}

