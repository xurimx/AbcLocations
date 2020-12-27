using Locations.Application.Accounts.Commands;
using Locations.Application.Accounts.Queries;
using Locations.Application.Accounts.Responses;
using Locations.Application.Cities.Commands;
using Locations.Application.Cities.Queries;
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
    public class CitiesController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<City>), 200)]
        public async Task<ActionResult<List<City>>> GetAll()
        {
            return await Mediator.Send(new GetAllCitiesQuery());
        }

        [HttpGet("query")]
        [ProducesResponseType(typeof(City), 200)]
        public async Task<ActionResult<City>> Get([FromQuery]string name)
        {
            return await Mediator.Send(new GetCityByNameQuery { Name = name});
        }

        [HttpPost]
        [ProducesResponseType(typeof(City), 200)]
        public async Task<ActionResult<City>> Post(CreateCityCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteCityCommand { Id = id});
            return Ok();
        }
    }
}
