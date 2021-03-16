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
        // get a report for a single day
        // get a report for a period of time (one month)
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get report by a day
        [HttpGet] // set name
        [Consumes(MediaTypeNames.Application.Json)]
        // Produces response type
        public async Task<IActionResult> GetReportByDay([FromQuery] GetReportByDate.Query query)
        {
            var response = await _mediator.Send(query);

            return Ok(response.Content);
        }
        
    }
}
