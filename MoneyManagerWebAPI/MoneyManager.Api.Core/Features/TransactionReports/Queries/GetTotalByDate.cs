using AutoMapper;
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
    /// <summary>
    /// Represents GetTotalByDate CQRS container class.
    /// </summary>
    public class GetTotalByDate
    {
        public class Query : IRequest<Response>
        {
            /// <summary>
            /// Represents the report date.
            /// </summary>
            [Required]
            public DateTime ReportDate { get; set; }
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
                var transactions = await _unitOfWork.Transactions.GetByDateAsync(request.ReportDate);

                if (transactions.Count == 0)
                {
                    return null;
                }

                var response = new Response();

                var transactionReport = new TransactionReport();
                transactionReport.Transactions.AddRange(transactions);

                var calculableReport = new CalculableTransactionReport(transactionReport);
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
