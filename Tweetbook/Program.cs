using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tweetbook.Data;

namespace Tweetbook
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            // Will do "update-database" when running.
            // Do __not__ do in production!!!
            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
                await dbContext.Database.MigrateAsync();

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await roleManager.RoleExistsAsync("Admin"))
                {
                    var adminRole = new IdentityRole("Admin");
                    await roleManager.CreateAsync(adminRole);
                }
                if (!await roleManager.RoleExistsAsync("Poster"))
                {
                    var posterRole = new IdentityRole("Poster");
                    await roleManager.CreateAsync(posterRole);
                }

                var password = "Foobar!0";
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                if(await userManager.FindByEmailAsync("poster@gmail.com") == null)
                {
                    var newUserId = Guid.NewGuid();
                    var newUser = new IdentityUser
                    {
                        Id = newUserId.ToString(),
                        Email = "poster@gmail.com",
                        UserName = "poster@gmail.com"
                    };

                    await userManager.CreateAsync(newUser, password);
                    await userManager.AddToRoleAsync(newUser, "Poster");
                }
                if (await userManager.FindByEmailAsync("test@gmail.com") == null)
                {
                    var newUserId = Guid.NewGuid();
                    var newUser = new IdentityUser
                    {
                        Id = newUserId.ToString(),
                        Email = "test@gmail.com",
                        UserName = "test@gmail.com"
                    };

                    await userManager.CreateAsync(newUser, password);
                }
                if (await userManager.FindByEmailAsync("admin@gmail.com") == null)
                {
                    var newUserId = Guid.NewGuid();
                    var newUser = new IdentityUser
                    {
                        Id = newUserId.ToString(),
                        Email = "admin@gmail.com",
                        UserName = "admin@gmail.com"
                    };

                    await userManager.CreateAsync(newUser, password);
                    await userManager.AddToRoleAsync(newUser, "Admin");
                }
            }

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
