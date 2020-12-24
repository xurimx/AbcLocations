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
    public class GetCityByNameQuery : IRequest<City>
    {
        public string Name { get; set; }
    }

    public class GetCityByNameQueryHandler : IRequestHandler<GetCityByNameQuery, City>
    {
        private readonly IAppDbContext context;

        public GetCityByNameQueryHandler(IAppDbContext context)
        {
            this.context = context;
        }
        public async Task<City> Handle(GetCityByNameQuery request, CancellationToken cancellationToken)
        {
            City city = await context.Cities.Where(x => x.Name.Equals(request.Name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }
    }
}
