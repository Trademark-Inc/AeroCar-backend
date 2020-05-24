using AeroCar.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models
{
    public static class DataSeeder
    {
        public static async Task CreateRolesAndAdmin(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            //adding customs roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<RegularUser>>();

            string[] roleNames = { "SystemAdmin", "AvioAdmin", "CarAdmin", "RegularUser" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist) await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // creating a super user who could maintain the web app
            var admin = new RegularUser
            {
                UserName = configuration.GetSection("AdminData")["Username"],
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Status = UserStatus.Activated
            };

            string userPassword = configuration.GetSection("AdminData")["Password"];
            var foundAdmin = await userManager.FindByNameAsync(admin.UserName) != null;

            if (!foundAdmin)
            {
                var createdAdmin = await userManager.CreateAsync(admin, userPassword);
                if (createdAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "SystemAdmin");
                }
            }
        }
    }
}
