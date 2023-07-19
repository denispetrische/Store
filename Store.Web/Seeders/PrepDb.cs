using Microsoft.CodeAnalysis;
using Store.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;

namespace Store.Web.Seeders
{
    public static class PrepDb
    {
        public static async void PrepDbInfo(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                //Create roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Manager", "Client"};

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                //Create users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                //Seed Admin
                string email = "admin@mail.ru";
                string password = "-Qwer1234";

                if (await userManager.FindByEmailAsync(email) == null )
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                }

                //Seed Manager
                email = "manager@mail.ru";
                password = "-Qwer1234";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Manager");
                }
            }
        }
    }
}
