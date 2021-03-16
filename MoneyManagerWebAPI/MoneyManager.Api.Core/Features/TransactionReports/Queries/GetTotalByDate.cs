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
    public class GetTotalByDate
    {
        public class Query : IRequest<Response>
        {
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
                var transactions = _unitOfWork.Transactions.GetByDay(request.ReportDate);

                if (transactions.Count == 0)
                {
                    return null;
                }
                var response = new Response();

                var report = new TransactionReport(transactions);
                // TODO: Change for decorated report
                response.Content = report.CalculateTotal();

                return response;
            }
        }

        public class Response
        {
            public int Content { get; set; }
        }
    }
}
