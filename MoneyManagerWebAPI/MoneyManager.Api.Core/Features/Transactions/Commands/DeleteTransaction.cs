using MediatR;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Transactions.Commands
{
    public static class DeleteTransaction
    {
        public class Command : IRequest<Response>
        {
            public int Id { get; set; }

            public Command(int id)
            {
                Id = id;
            }
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
                var transaction = await _unitOfWork.Transactions.FindAsync(t => t.Id == request.Id);

                var response = new Response();

                if (transaction == null)
                {
                    response.Message = "Transaction does not exist!";
                    response.StatusCode = 400;

                    return response;
                }

                await _unitOfWork.Transactions.RemoveAsync(transaction);

                return response;
            }
        }

        public class Response
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}
