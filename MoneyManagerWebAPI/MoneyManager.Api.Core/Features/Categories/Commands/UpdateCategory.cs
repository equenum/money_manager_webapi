using MediatR;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Categories.Commands
{
    public static class UpdateCategory
    {
        public class Command : IRequest<Response>
        {
            public int Id { get; set; }

            [MaxLength(255)]
            public string Name { get; set; }

            [MaxLength(2000)]
            public string Description { get; set; }
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

                if (request.Name == null && request.Description == null)
                {
                    response.Message = "There is no argument given!";
                    response.StatusCode = 400;

                    return response;
                }

                if (request.Name != null)
                {
                    category.Name = request.Name;
                    category.Modified = DateTime.Now;
                }

                if (request.Description != null)
                {
                    category.Description = request.Description;
                    category.Modified = DateTime.Now;
                }

                _unitOfWork.Complete();

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
