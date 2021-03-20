using MediatR;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Users.Commands
{
    /// <summary>
    /// Represents DeleteUser CQRS container class.
    /// </summary>
    public static class DeleteUser
    {
        public class Command : IRequest<Response>
        {
            /// <summary>
            /// Represents the user's id.
            /// </summary>
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
                var user = await _unitOfWork.Users.FindAsync(u => u.Id == request.Id);

                var response = new Response();

                if (user == null)
                {
                    response.Message = "User does not exist!";
                    response.StatusCode = 400;

                    return response;
                }

                await _unitOfWork.Users.RemoveAsync(user);

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
