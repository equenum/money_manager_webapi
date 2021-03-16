using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Api.Core.Dtos.Transaction;
using MoneyManager.Api.Core.Features.Transactions.Commands;
using MoneyManager.Api.Core.Features.Transactions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MoneyManager.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets transactions (paginated).
        /// </summary>
        /// <returns>A list of transactions according to the specified page parameters.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetAllTransactions.Query query)
        {
            var response = await _mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        /// <summary>
        /// Gets a transaction by id.
        /// </summary>
        /// <param name="id">Transaction id.</param>
        /// <returns>A transaction with the specified id.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetTransactionById.Query(id));

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <returns>The newly created transaction.</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TransactionDto))]
        public async Task<IActionResult> Post([FromBody] CreateTransaction.Command command)
        {
            var response = await _mediator.Send(command);
            var transactionDto = response.Content;

            return CreatedAtAction(nameof(GetById), new { id = transactionDto.Id }, transactionDto);
        }

        /// <summary>
        /// Updates a transaction with the specified id.
        /// </summary>
        /// <param name="id">Request transaction id.</param>
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateTransaction.Response))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdateTransaction.Command command)
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
        /// Deletes a transaction with the specified id.
        /// </summary>
        /// <param name="id">Transaction id.</param>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DeleteTransaction.Response))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteTransaction.Command(id));

            if (response.Message != null)
            {
                return StatusCode(response.StatusCode, response);
            }

            return NoContent();
        }
    }
}
