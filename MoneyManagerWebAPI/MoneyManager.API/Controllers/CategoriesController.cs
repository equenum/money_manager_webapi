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

            return Ok(response.Content);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetById.Query(id));

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        [HttpPost]
        // [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Post(CreateCategory.Command command)
        {
            var response = await _mediator.Send(command);

            if (response.Content == null)
            {
                return StatusCode(response.StatusCode, response);
            }

            var categoryDto = response.Content;

            return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateCategory.Command command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);

            if (response.Message != null)
            {
                return StatusCode(response.StatusCode, response);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCategory.Command(id));

            if (response.Message != null)
            {
                return StatusCode(response.StatusCode, response);
            }

            return NoContent();
        }
    }
}
