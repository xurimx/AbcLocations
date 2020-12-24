using Locations.Application.Accounts.Responses;
using Locations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateToken(User user);
        Task<TokenResponse> RefreshToken(string token, string refreshToken);
        Task<bool> CheckPassword(string username, string password);
    }
}
