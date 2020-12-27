using Locations.Application.Common.Exceptions;
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
    public class CreateCityCommand : IRequest<City>
    {
        public string Name { get; set; }
    }

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, City>
    {
        private readonly IAppDbContext context;

        public CreateCityCommandHandler(IAppDbContext context)
        {
            this.context = context;
        }
        public async Task<City> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            City city = await context.Cities.FirstOrDefaultAsync(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            if (city == null)
            {
                city = new City { Name = request.Name };
                context.Cities.Add(city);
                await context.SaveChangesAsync();
                return city;
            }
            throw new LocationException($"{request.Name} already exists");
        }
    }
}
