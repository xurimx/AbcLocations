using Locations.Application.Common.Interfaces;
using Locations.Application.Common.Models;
using Locations.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Locations.Application.Locations.Queries
{
    public class GetLocationsQuery : IRequest<Pagination<Location>>, IRequestQuery
    {
        public string Search { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; } = "desc";
        public int? Page { get; set; } = 1;
        public int? Limit { get; set; } = 10;
    }

    public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, Pagination<Location>>
    {
        private readonly IAppDbContext context;

        public GetLocationsQueryHandler(IAppDbContext context)
        {
            this.context = context;
        }
        public async Task<Pagination<Location>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            int page = request.Page ?? 1;
            int limit = request.Limit ?? 10;
            int total = await context.Locations.CountAsync();

            var query = context.Locations.Include(x => x.City).AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search) ||
                                         x.Address.Contains(request.Search) ||
                                         x.City.Name.Contains(request.Search));
            }

            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                string column = request.OrderBy;
                string direction = request.OrderDirection == "desc" ? "desc" : "asc";
                query = query.OrderBy(column + " " + direction);
            }
            else
            {
                query = query.OrderBy("created desc");
            }

            int filtered = query.Count();
            List<Location> items = await query.Skip((page - 1) * limit).Take(limit).ToListAsync();

            Pagination<Location> pagination = new Pagination<Location>
            {
                TotalItems = await context.Locations.CountAsync(),
                FilteredItems = filtered,
                CurrentPage = page,
                Items = items,
                TotalPages = (int)Math.Ceiling((double)filtered / limit)
            };
            return pagination;
        }
    }
}
