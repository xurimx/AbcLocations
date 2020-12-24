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

namespace Locations.Application.Locations.Commands
{
    public class DeleteLocationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
    {
        private readonly IAppDbContext context;

        public DeleteLocationCommandHandler(IAppDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            Location location = await context.Locations.FirstOrDefaultAsync(x => x.Id == request.Id);
            context.Locations.Remove(location);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
