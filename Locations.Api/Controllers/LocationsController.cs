using Locations.Application.Common.Models;
using Locations.Application.Locations.Commands;
using Locations.Application.Locations.Queries;
using Locations.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LocationsController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(Pagination<Location>), 200)]
        public async Task<ActionResult<Pagination<Location>>> Query([FromQuery]GetLocationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Location), 200)]
        public async Task<ActionResult<Location>> Get(Guid id)
        {
            return await Mediator.Send(new GetLocationByIdQuery { Id = id});
        }

        [HttpPost]
        [ProducesResponseType(typeof(Location), 200)]
        public async Task<ActionResult<Location>> CreateLocation(CreateLocationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Location), 200)]
        public async Task<ActionResult<Location>> UpdateLocation(UpdateLocationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteLocation(Guid id)
        {
            await Mediator.Send(new DeleteLocationCommand { Id = id });
            return Ok();
        }
    }
}
