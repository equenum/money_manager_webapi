using AutoMapper;
using MediatR;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Dtos.Transaction;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Transactions.Commands
{
    public static class CreateTransaction
    {
        public class Command : IRequest<Response>
        {
            [Required]
            [EnumDataType(typeof(TransactionType))]
            public TransactionType Type { get; set; }

            [Required]
            public int CategoryId { get; set; }

            [MaxLength(2000)]
            public string Description { get; set; }

            [Required]
            [Range(1, 20000)]
            public int Amount { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var transaction = _mapper.Map<Transaction>(request);

                var response = new Response();

                transaction.Created = DateTime.Now;
                transaction.Modified = DateTime.Now;
                _unitOfWork.Transactions.Add(transaction);

                var createdTransaction = _unitOfWork.Transactions.GetNewelyCreated(transaction.Created, transaction.CategoryId, transaction.Amount);
                response.Content = _mapper.Map<TransactionDto>(createdTransaction);

                return response;
            }
        }

        public class Response
        {
            public TransactionDto Content { get; set; }
        }
    }
}
