﻿using AutoMapper;
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

namespace MoneyManager.Api.Core.Features.Users.Queries
{
    /// <summary>
    /// Represents AuthenticateUser CQRS container class.
    /// </summary>
    public static class AuthenticateUser
    {
        public class Query : IRequest<Response>
        {
            /// <summary>
            /// Represents the user's username.
            /// </summary>
            [Required]
            [MaxLength(20)]
            public string Username { get; set; }

            /// <summary>
            /// Represents the user's password.
            /// </summary>
            [Required]
            [MaxLength(20)]
            public string Password { get; set; }
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
                var requestUser = _mapper.Map<User>(request);
                var existingUser = await _unitOfWork.Users.FindAsync(u => u.Username == requestUser.Username);

                var response = new Response();

                if (existingUser == null)
                {
                    response.Message = "User doesn't exist!";
                    response.StatusCode = 404;

                    return response;
                }

                var targetUser = await _unitOfWork.Users.AuthenticateAsync(requestUser.Username, requestUser.Password);

                if (targetUser == null)
                {
                    response.Message = "Incorrect password!";
                    response.StatusCode = 401;

                    return response;
                }

                response.Content = _mapper.Map<UserAuthDto>(targetUser);

                return response;
            }
        }

        public class Response
        {
            public UserAuthDto Content { get; set; }
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}
