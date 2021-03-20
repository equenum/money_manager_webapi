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
    /// <summary>
    /// Represents RegisterUser CQRS container class.
    /// </summary>
    public static class RegisterUser
    {
        public class Command : IRequest<Response>
        {
            /// <summary>
            /// Represents the new user's username.
            /// </summary>
            [Required]
            [MaxLength(20)]
            public string Username { get; set; }

            /// <summary>
            /// Represents the new user's password.
            /// </summary>
            [Required]
            [MaxLength(20)]
            public string Password { get; set; }

            /// <summary>
            /// Represents the new user's role.
            /// </summary>
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

                var existingUser = await _unitOfWork.Users.FindAsync(u => u.Username == user.Username);

                var response = new Response();

                if (existingUser != null)
                {
                    response.Message = "Username already exists!";
                    response.StatusCode = 409;

                    return response;
                }

                var createdUser = await _unitOfWork.Users.RegisterAsync(user);
                
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
