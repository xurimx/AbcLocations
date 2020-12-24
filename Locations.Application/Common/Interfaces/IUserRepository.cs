using Locations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(string username, string email, string password, string role);
        Task<User> FindByUsernameAsync(string username);
        Task<User> GetUserById(string id);
    }
}
