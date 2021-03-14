using AutoMapper;
using MediatR;
using MoneyManager.Api.Core.Dtos.Category;
using MoneyManager.Api.Core.Interfaces;
using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Categories.Queries
{
    public static class GetAllCategories
    {
        public class Query : IRequest<Response>
        {
            /// <summary>
            /// Request page number.
            /// </summary>
            public int PageNumber { get; set; } = 1;
            /// <summary>
            /// Request page size.
            /// </summary>
            public int PageSize { get; set; } = 10;
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
                var categories = _unitOfWork.Categories.GetAllPaged(c => c.Id, request.PageNumber, request.PageSize);

                if (categories.Count == 0)
                {
                    return null;
                }
                
                var response = new Response();

                foreach (var category in categories)
                {
                    response.Content.Add(_mapper.Map<CategoryDto>(category));
                }

                return response;
            }
        }

        public class Response
        {
            public ICollection<CategoryDto> Content { get; set; } = new List<CategoryDto>();
        }
    }
}
