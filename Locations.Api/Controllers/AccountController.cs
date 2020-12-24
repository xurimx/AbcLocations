using Locations.Application.Accounts.Commands;
using Locations.Application.Accounts.Queries;
using Locations.Application.Accounts.Responses;
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
    public class AccountController : BaseApiController
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult<User>> Register(RegisterCommand register)
        {
            return await Mediator.Send(register);
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public async Task<ActionResult<TokenResponse>> Authenticate(AuthenticateRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        public async Task<ActionResult<TokenResponse>> Refresh(RefreshTokenRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpGet("userinfo")]
        [ProducesResponseType(typeof(User), 200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<User>> Userinfo()
        {
            return await Mediator.Send(new GetCurrentUserQuery { });
        }
    }
}
