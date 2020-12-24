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

namespace Locations.Application.Cities.Queries
{
    public class GetAllCitiesQuery : IRequest<List<City>>
    {
        
    }

    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, List<City>>
    {
        private readonly IAppDbContext context;

        public GetAllCitiesQueryHandler(IAppDbContext context)
        {
            this.context = context;
        }
        public async Task<List<City>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            List<City> cities = await context.Cities.ToListAsync();
            return cities;
        }
    }
}
