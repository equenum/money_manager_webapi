using MediatR;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.TransactionReports.Queries
{
    public static class GetTotalByPeriod
    {
        public class Query : IRequest<Response>
        {
            [Required]
            public DateTime BeginningDate { get; set; }

            [Required]
            public DateTime EndDate { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var transactions = await _unitOfWork.Transactions.GetByPeriodAsync(request.BeginningDate, request.EndDate);

                if (transactions.Count == 0)
                {
                    return null;
                }

                var response = new Response();
                
                var calculableReport = new CalculableTransactionReport(new TransactionReport(transactions));
                response.Content = calculableReport.CalculateTotal();

                return response;
            }
        }

        public class Response
        {
            public int Content { get; set; }
        }
    }
}
