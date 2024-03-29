﻿using AutoMapper;
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
    /// <summary>
    /// Represents CreateTransaction CQRS container class.
    /// </summary>
    public static class CreateTransaction
    {
        public class Command : IRequest<Response>
        {
            /// <summary>
            /// Represents the transaction type.
            /// </summary>
            [Required]
            [EnumDataType(typeof(TransactionType))]
            public TransactionType Type { get; set; }

            /// <summary>
            /// Represents the transaction category id.
            /// </summary>
            [Required]
            public int CategoryId { get; set; }

            /// <summary>
            /// Represents the transaction description.
            /// </summary>
            [MaxLength(2000)]
            public string Description { get; set; }

            /// <summary>
            /// Represents the transaction amount.
            /// </summary>
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

                var createdTransaction = await _unitOfWork.Transactions.AddAsync(transaction);
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
