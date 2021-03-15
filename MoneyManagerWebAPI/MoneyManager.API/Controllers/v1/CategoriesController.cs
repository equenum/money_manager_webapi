using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Api.Core.Dtos.Category;
using MoneyManager.Api.Core.Features.Categories.Commands;
using MoneyManager.Api.Core.Features.Categories.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MoneyManager.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets categories (paginated).
        /// </summary>
        /// <returns>A list of categories according to the specified page parameters.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetAllCategories.Query query)
        {
            var response = await _mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        /// <summary>
        /// Gets a category by id.
        /// </summary>
        /// <param name="id">Category id.</param>
        /// <returns>A category with the specified id.</returns>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetById.Query(id));

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <returns>The newly  created category.</returns>
        [HttpPost]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(CreateCategory.Response))]
        public async Task<IActionResult> Post([FromBody] CreateCategory.Command command)
        {
            var response = await _mediator.Send(command);

            if (response.Content == null)
            {
                return StatusCode(response.StatusCode, response);
            }

            var categoryDto = response.Content;

            return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
        }

        /// <summary>
        /// Updates a category with the specified id.
        /// </summary>
        /// <param name="id">Request category id.</param>
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateCategory.Response))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

        /// <summary>
        /// Deletes a category with the specified id.
        /// </summary>
        /// <param name="id">Category id.</param>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DeleteCategory.Response))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
