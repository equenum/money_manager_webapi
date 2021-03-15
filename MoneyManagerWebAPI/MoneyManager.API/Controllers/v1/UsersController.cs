using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Api.Core.Dtos.User;
using MoneyManager.Api.Core.Features.Users.Commands;
using MoneyManager.Api.Core.Features.Users.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Authorizes the user.
        /// </summary>
        /// <returns>The specified user record along with a generated JWT token.</returns>
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateUser.Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(AuthenticateUser.Response))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(AuthenticateUser.Response))]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUser.Query query)
        {
            var response = await _mediator.Send(query);

            if (response.Message != null)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response.Content);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <returns>The newely created user.</returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(RegisterUser.Response))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
        public async Task<IActionResult> Register([FromBody] RegisterUser.Command command)
        {
            var response = await _mediator.Send(command);

            if (response.Content == null)
            {
                return StatusCode(response.StatusCode, response);
            }

            var userDto = response.Content;

            return CreatedAtAction(null, new { id = userDto.Id }, userDto);
        }
    }
}
