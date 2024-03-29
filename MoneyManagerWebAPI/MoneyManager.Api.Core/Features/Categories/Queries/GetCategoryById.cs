﻿using AutoMapper;
using MediatR;
using MoneyManager.Api.Core.Dtos.Category;
using MoneyManager.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Categories.Queries
{
    /// <summary>
    /// Represents GetCategoryById CQRS container class.
    /// </summary>
    public static class GetCategoryById
    {
        public class Query : IRequest<Response>
        {
            /// <summary>
            /// Represents the category id.
            /// </summary>
            public int Id { get; set; }

            public Query(int id) => Id = id;
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
                var category = await _unitOfWork.Categories.GetAsync(request.Id);

                if (category == null)
                {
                    return null;
                }

                var response = new Response
                {
                    Content = _mapper.Map<CategoryDto>(category)
                };

                return response;
            }
        }

        public class Response
        {
            public CategoryDto Content { get; set; }
        }
    }
}
