using Locations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<City> Cities { get; set; }
        Task<int> SaveChangesAsync();
    }
}
