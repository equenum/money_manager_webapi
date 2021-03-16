using AutoMapper;
using MediatR;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Categories.Commands
{
    public static class DeleteCategory
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
                var category = await _unitOfWork.Categories.FindAsync(c => c.Id == request.Id);

                var response = new Response();

                if (category == null)
                {
                    response.Message = "Category does not exist!";
                    response.StatusCode = 400;

                    return response;
                }

                await _unitOfWork.Categories.RemoveAsync(category);

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
