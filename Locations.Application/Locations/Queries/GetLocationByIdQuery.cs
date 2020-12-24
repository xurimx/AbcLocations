using Locations.Application.Common.Interfaces;
using Locations.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Locations.Application.Locations.Queries
{
    public class GetLocationByIdQuery : IRequest<Location>
    {
        public Guid Id { get; set; }
    }

    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, Location>
    {
        private readonly IAppDbContext context;

        public GetLocationByIdQueryHandler(IAppDbContext context)
        {
            this.context = context;
        }
        public async Task<Location> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            Location location = await context.Locations.FirstOrDefaultAsync(x => x.Id == request.Id);
            return location;
        }
    }
}
