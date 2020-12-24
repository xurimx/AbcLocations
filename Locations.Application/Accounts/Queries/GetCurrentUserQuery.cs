using Locations.Application.Common.Interfaces;
using Locations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Locations.Application.Accounts.Queries
{
    public class GetCurrentUserQuery : IRequest<User>
    {
        
    }
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, User>
    {
        private readonly ICurrentUserService service;
        private readonly IUserRepository repository;

        public GetCurrentUserQueryHandler(ICurrentUserService service, IUserRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }
        public Task<User> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            return repository.GetUserById(service.UserId);
        }
    }
}
