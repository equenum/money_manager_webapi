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
    public static class GetTransactionById
    {
        public class Query : IRequest<Response>
        {
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
                var transaction = _unitOfWork.Transactions.Get(request.Id);

                if (transaction == null)
                {
                    return null;
                }

                var response = new Response();
                response.Content = _mapper.Map<TransactionDto>(transaction);

                return response;
            }
        }

        public class Response
        {
            public TransactionDto Content { get; set; }
        }
    }
}
