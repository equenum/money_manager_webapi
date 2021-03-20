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
    /// <summary>
    /// Represents GetTransactionById CQRS container class.
    /// </summary>
    public static class GetTransactionById
    {
        public class Query : IRequest<Response>
        {
            /// <summary>
            /// Represents the transaction's id.
            /// </summary>
            public int Id { get; set; }

            public Query(int id) => Id = id;
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
                var transaction = await _unitOfWork.Transactions.GetAsync(request.Id);

                if (transaction == null)
                {
                    return null;
                }

                var response = new Response
                {
                    Content = _mapper.Map<TransactionDto>(transaction)
                };

                return response;
            }
        }

        public class Response
        {
            public TransactionDto Content { get; set; }
        }
    }
}
