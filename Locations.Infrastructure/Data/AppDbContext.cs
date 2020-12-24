using Locations.Application.Common.Interfaces;
using Locations.Domain.Common;
using Locations.Domain.Entities;
using Locations.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        private readonly ICurrentUserService currentUserService;

        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<City> Cities { get; set; }

        public Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {                
                switch (entry.State)
                {                   
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = currentUserService.UserId;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentUserService.UserId;
                        entry.Entity.Created = DateTime.UtcNow;
                        break;
                    default: break;
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
