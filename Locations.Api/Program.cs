using Locations.Domain.Entities;
using Locations.Infrastructure.Data;
using Locations.Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            CreateDefaultAccountAsync(host).Wait();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task CreateDefaultAccountAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();                
                ctx.Database.Migrate();
                //bool v = ctx.Database.EnsureCreated();

                bool hasCities = await ctx.Cities.AnyAsync();
                if (!hasCities)
                {
                    SeddDefaultCities(ctx);
                    ctx.SaveChanges();
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                ApplicationUser admin = await userManager.FindByNameAsync("admin");
                IdentityRole adminRole = await roleManager.FindByNameAsync("admin");
                if (adminRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("admin"));
                }
                IdentityRole userRole = await roleManager.FindByNameAsync("user");
                if (userRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("user"));
                }
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@test.com",
                    };
                    IdentityResult result = await userManager.CreateAsync(admin, "P@ssw0rd");
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException("Error creating default user");
                    }
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }

        private static void SeddDefaultCities(AppDbContext ctx)
        {
            ctx.Cities.AddRange(new City[]
            {
                new City { Name = "Berlin", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Amsterdam", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Ljubljana", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Belgrade", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Zagreb", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Sarajevo", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Prishtina", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Rome", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Paris", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Madrid", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Istanbul", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Moscow", Created = DateTime.UtcNow,  CreatedBy = "seeder" },
                new City { Name = "Stockholm", Created = DateTime.UtcNow,  CreatedBy = "seeder" }
            });
        }
    }
}
