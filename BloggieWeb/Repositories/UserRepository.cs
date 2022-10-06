using System;
using BloggieWeb.Data;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AuthDbContext _authDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(AuthDbContext authDbContext, UserManager<IdentityUser> userManager)
        {
            _authDbContext = authDbContext;
            _userManager = userManager;
        }

        public async Task<bool> Add(IdentityUser identityUser, string password, List<string> roles)
        {
            var identityResult = await _userManager.CreateAsync(identityUser, password);

            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRolesAsync(identityUser, roles);

                if (identityResult.Succeeded)
                {
                    return true;
                }

            }
            return false;
        }

        public async Task Delete(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await _authDbContext.Users.ToListAsync();
            // not shoe super admin
            var superAdminUser = await _authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");

            // remove superadmin from users
            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }

            return users;
        }

    }
}
