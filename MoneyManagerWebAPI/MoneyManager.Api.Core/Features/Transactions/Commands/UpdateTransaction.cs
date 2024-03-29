﻿using MediatR;
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
    /// Represents UpdateTransaction CQRS container class.
    /// </summary>
    public static class UpdateTransaction
    {
        public class Command : IRequest<Response>
        {
            /// <summary>
            /// Representa the transaction's id.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Representa the transaction's type.
            /// </summary>
            [EnumDataType(typeof(TransactionType))]
            public TransactionType Type { get; set; }

            /// <summary>
            /// Representa the transaction's category id.
            /// </summary>
            public int CategoryId { get; set; }

            /// <summary>
            /// Representa the transaction's description.
            /// </summary>
            [MaxLength(2000)]
            public string Description { get; set; }

            /// <summary>
            /// Representa the transaction's amount.
            /// </summary>
            [Range(1, 20000)]
            public int Amount { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var transaction = await _unitOfWork.Transactions.FindAsync(c => c.Id == request.Id);

                var response = new Response();

                if (transaction == null)
                {
                    response.Message = "Transaction does not exist!";
                    response.StatusCode = 400;

                    return response;
                }

                if (ArgumentsAreNotGiven(request))
                {
                    response.Message = "There is no argument given!";
                    response.StatusCode = 400;

                    return response;
                }

                if (request.Type != 0)
                {
                    transaction.Type = request.Type;
                    transaction.Modified = DateTime.Now;
                }

                if (request.CategoryId != 0)
                {
                    transaction.CategoryId = request.CategoryId;
                    transaction.Modified = DateTime.Now;
                }

                if (request.Description != null)
                {
                    transaction.Description = request.Description;
                    transaction.Modified = DateTime.Now;
                }

                if (request.Amount != 0)
                {
                    transaction.Amount = request.Amount;
                    transaction.Modified = DateTime.Now;
                }

                _unitOfWork.Complete();

                return response;
            }

            private bool ArgumentsAreNotGiven(Command request)
            {
                return request.Type == 0 && 
                    request.CategoryId == 0 && 
                    request.Description == null && 
                    request.Amount == 0;
            }
        }

        public class Response
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}
