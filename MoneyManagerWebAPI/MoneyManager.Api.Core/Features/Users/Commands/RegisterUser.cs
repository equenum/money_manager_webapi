using AutoMapper;
using MediatR;
using MoneyManager.Api.Core.Domain.Entities.Authentication;
using MoneyManager.Api.Core.Dtos.User;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Users.Commands
{
    public static class RegisterUser
    {
        public class Command : IRequest<Response>
        {
            [Required]
            [MaxLength(20)]
            public string Name { get; set; }

            [Required]
            [MaxLength(20)]
            public string Password { get; set; }

            [Required]
            [MaxLength(20)]
            public string Role { get; set; }
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
                var user = _mapper.Map<User>(request);
                user.Role = user.Role.ToUpperInvariant();

                var existingUser = _unitOfWork.Users.Find(user.Name);

                var response = new Response();

                if (existingUser != null)
                {
                    response.Message = "Username already exists!";
                    response.StatusCode = 409;

                    return response;
                }

                _unitOfWork.Users.Register(user.Name, user.Password, user.Role);
                var createdUser = _unitOfWork.Users.Find(user.Name);

                response.Content = _mapper.Map<UserDto>(createdUser);

                return response;
            }
        }

        public class Response
        {
            public UserDto Content { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}
