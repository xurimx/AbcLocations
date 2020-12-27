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

namespace Locations.Application.Locations.Commands
{
    public class CreateLocationCommand : IRequest<Location>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string City { get; set; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Location>
    {
        private readonly IAppDbContext context;

        public CreateLocationCommandHandler(IAppDbContext context)
        {
            this.context = context;
        }
        public async Task<Location> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            City city = await context.Cities.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(request.City.ToLower()));
            if (city == null)
            {
                throw new LocationException("An Error has occurred while getting the city");
            }
            Location location = new Location
            {
                Address = request.Address,
                CityId = city.Id,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Name = request.Name
            };
            context.Locations.Add(location);
            await context.SaveChangesAsync();
            return location;

        }
    }
}
