using Locations.Application.Accounts.Responses;
using Locations.Application.Common.Exceptions;
using Locations.Application.Common.Interfaces;
using Locations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Locations.Application.Accounts.Commands
{
    public class AuthenticateRequest : IRequest<TokenResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateRequestHandler : IRequestHandler<AuthenticateRequest, TokenResponse>
    {
        private readonly IAuthenticationService authentication;
        private readonly IUserRepository repository;
        public AuthenticateRequestHandler(IUserRepository repository, IAuthenticationService authentication)
        {
            this.repository = repository;
            this.authentication = authentication;
        }
        public async Task<TokenResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            User user = await repository.FindByUsernameAsync(request.Username);
            if (user == null)
            {
                throw new LocationException("Username or password is incorrect.");
            }

            bool result = await authentication.CheckPassword(request.Username, request.Password);

            if (result == false)
            {
                throw new LocationException("Username or password is incorrect.");
            }
            return await authentication.CreateToken(user);
        }
    }
}
