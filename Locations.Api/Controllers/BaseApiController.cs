using Locations.Api.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
