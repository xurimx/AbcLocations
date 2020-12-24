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
    public class RegisterCommand : IRequest<User>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, User>
    {
        private readonly IUserRepository repository;

        public RegisterCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User user = await repository.FindByUsernameAsync(request.Username);
            if (user != null)
            {
                throw new LocationException($"A User with {request.Username} already exists");
            }
            user = await repository.CreateAsync(request.Username, request.Email, request.Password, "user");
            return user;
        }
    }
}
