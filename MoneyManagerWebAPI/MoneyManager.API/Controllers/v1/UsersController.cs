using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Api.Core.Domain.Entities.Authentication;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Api.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] User user)
        {
            // TODO: Implement
            return Ok(user);
        }
    }
}
