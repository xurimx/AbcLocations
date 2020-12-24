using Locations.Application.Common.Interfaces;
using Locations.Infrastructure.Data;
using Locations.Infrastructure.Models;
using Locations.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseInMemoryDatabase("dev");
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IAppDbContext>(cfg => cfg.GetRequiredService<AppDbContext>());

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication()
                .AddJwtBearer(config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = configuration["Tokens:Issuer"],
                        ValidAudience = configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMapsService, MapsService>();

            return services;
        }
    }
}
