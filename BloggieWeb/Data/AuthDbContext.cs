using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(IdentityDbContext options) : base(options)
        {
        }


        //Seed Roles (User, Admin, Super Admin)

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = "5d3aab8f-98bf-44da-8f3d-c616a4bdf0b0";
            var adminRoleId = "ae7bd852-9ac4-4eba-aa4f-71e12fad6fd6";
            var userRoleId = "415a91b7-5cfe-4c63-9eda-2cc7881e393e";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole()
                {
                    Name = "admin",
                    NormalizedName = "admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole()
                {
                    Name = "user",
                    NormalizedName = "user",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            }
        }

        // Seed Super Admin User

        // Add ALl Roles To Super Admin User
    }
}

