using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Api.Core.Features.Categories.Commands;
using MoneyManager.Api.Core.Features.Categories.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetAllCategories.Query query)
        {
            var response = await _mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCategory.Command command)
        {
            // Probably should return Created 201
            // Validate post later
            return Ok(await _mediator.Send(command));
        }

        // Get
        // Get by Id.
        // Get paged.
    }
}
