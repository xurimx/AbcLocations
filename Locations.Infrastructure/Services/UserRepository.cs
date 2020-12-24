using Locations.Application.Common.Exceptions;
using Locations.Application.Common.Interfaces;
using Locations.Application.Common.Models;
using Locations.Domain.Entities;
using Locations.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Infrastructure.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<User> CreateAsync(string username, string email, string password, string role)
        {
            ApplicationUser appUser = await userManager.FindByNameAsync(username);
            if (appUser != null)
            {
                throw new LocationException($"A user with username: {username} already exists!", 400);
            }
            appUser = new ApplicationUser
            {
                UserName = username,
                Email = email
            };
            IdentityResult identityResult = await userManager.CreateAsync(appUser, password);
            if (!identityResult.Succeeded)
            {
                List<Error> errors = identityResult.Errors.Select(x => new Error { Type = x.Code, Description = x.Description }).ToList();
                throw new LocationException("User could not be created!", 400, errors);
            }
            await userManager.AddToRoleAsync(appUser, role);
            return await MapFrom(appUser);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            ApplicationUser appUser = await userManager.FindByNameAsync(username);

            if (appUser == null) return null;

            return await MapFrom(appUser);

        }

        public async Task<User> GetUserById(string id)
        {
            ApplicationUser appUser = await userManager.FindByIdAsync(id);

            if (appUser == null) return null;

            return await MapFrom(appUser);
        }

        private async Task<User> MapFrom(ApplicationUser user)
        {
            IList<string> roles = await userManager.GetRolesAsync(user);
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Role = roles.FirstOrDefault()
            };
        }
    }
}
