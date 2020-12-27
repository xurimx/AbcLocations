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
    public class UpdateLocationCommand : IRequest<Location>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string City { get; set; }
    }

    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Location>
    {
        private readonly IAppDbContext context;

        public UpdateLocationCommandHandler(IAppDbContext context)
        {
            this.context = context;
        }
        public async Task<Location> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            Location location = await context.Locations.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (location == null)
            {
                throw new LocationException("No location was found for id: " + request.Id);
            }

            City city = await context.Cities.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(request.City.ToLower()));
            if (city == null)
            {
                throw new LocationException("An Error has occurred while getting the city");
            }
            location.Address = request.Address;
            location.CityId = city.Id;
            location.Latitude = request.Latitude;
            location.Longitude = request.Longitude;
            location.Name = request.Name;
            context.Locations.Update(location);
            await context.SaveChangesAsync();
            return location;
        }
    }
}
