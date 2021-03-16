using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Api.Core.Features.TransactionReports.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MoneyManager.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        // get a report for a period of time (one month)
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a transaction report total amount for the specified date.
        /// </summary>
        /// <returns>Transaction report total amount for the specified date.</returns>
        [HttpGet("GetTotalByDate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTotalByDate.Response>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTotalByDate([FromQuery] GetTotalByDate.Query query)
        {
            var response = await _mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }

        /// <summary>
        /// Gets a transaction report total amount for the specified period of time.
        /// </summary>
        /// <returns>Transaction report total amount for the specified period of time.</returns>
        [HttpGet("GetTotalByPeriod")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTotalByPeriod.Response>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTotalByPeriod([FromQuery] GetTotalByPeriod.Query query)
        {
            var response = await _mediator.Send(query);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Content);
        }
    }
}
