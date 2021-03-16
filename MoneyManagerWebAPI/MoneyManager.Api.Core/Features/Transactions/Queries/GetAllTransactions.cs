using AutoMapper;
using MediatR;
using MoneyManager.Api.Core.Dtos.Transaction;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Transactions.Queries
{
    public static class GetAllTransactions
    {
        public class Query : IRequest<Response>
        {
            /// <summary>
            /// Request page number.
            /// </summary>
            public int PageNumber { get; set; } = 1;
            /// <summary>
            /// Request page size.
            /// </summary>
            public int PageSize { get; set; } = 10;
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var transactions = await _unitOfWork.Transactions.GetAllPagedAsync(t => t.Id, request.PageNumber, request.PageSize);

                if (transactions.Count == 0)
                {
                    return null;
                }

                var response = new Response();

                foreach (var transaction in transactions)
                {
                    response.Content.Add(_mapper.Map<TransactionDto>(transaction));
                }

                return response;
            }
        }

        public class Response
        {
            public ICollection<TransactionDto> Content { get; set; } = new List<TransactionDto>();
        }
    }
}
