using Microsoft.CodeAnalysis;
using Store.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Store.Web.Seeders
{
    public static class PrepDb
    {
        public static void PrepDbInfo(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<StoreWebContext>(), isProd);
            }
        }

        private static void SeedData(StoreWebContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("Attempting to apply migrations");

                try
                {
                    context.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Could not run migrations {e.Message}");
                }
            }

            if (!context.Users.Any())
            {
                Console.WriteLine("Seeding Data");

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Data already in Db");
            }
        }
    }
}
