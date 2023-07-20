using Microsoft.CodeAnalysis;
using Store.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using Store.Web.Models;
using Store.Web.Constants;
using Store.Web.Abstractions.Data;

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


                //Create Products

                var constants = new AppConstants();
                var products = new Product[]
                {
                    new Product()
                    {
                        Name = "Banana",
                        Description = "Yellow bananas from Ecuador",
                        ReceiptDate = DateTime.Now,
                        ExpireDate = DateTime.Now.Add(constants._expireTime),
                        Amount = 10,
                        Price = 10,
                        Currency = "BYN"
                    },
                    new Product()
                    {
                        Name = "Computer",
                        Description = "Powerfull and new",
                        ReceiptDate = DateTime.Now,
                        ExpireDate = DateTime.Now.Add(constants._expireTime),
                        Amount = 1,
                        Price = 1000,
                        Currency = "GBP"
                    },
                    new Product()
                    {
                        Name = "Mineral Water",
                        Description = "Made in Georgia",
                        ReceiptDate = DateTime.Now,
                        ExpireDate = DateTime.Now.Add(constants._expireTime),
                        Amount = 256,
                        Price = 5
                    }
                };

                var productRepo = serviceScope.ServiceProvider.GetRequiredService<IProductRepo>();

                if (!productRepo.GetProducts().Result.Any())
                {
                    foreach (var product in products)
                    {
                        productRepo.CreateProduct(product);
                    }
                }
            }
        }
    }
}
