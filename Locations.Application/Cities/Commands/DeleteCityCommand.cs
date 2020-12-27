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

namespace Locations.Application.Cities.Commands
{
    public class DeleteCityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, bool>
    {
        private readonly IAppDbContext context;

        public DeleteCityCommandHandler(IAppDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            City city = await context.Cities.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (city == null)
            {
                return false;
            }
            context.Cities.Remove(city);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
