using AutoMapper;
using MediatR;
using MoneyManager.Api.Core.Dtos.Category;
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
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ICategoryRepository _categoryRepository; // TODO: Wire up unit of work here
            private readonly IMapper _mapper;

            public Handler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                ValidateRequest(request);

                var categories = _categoryRepository.GetAllPaged(c => c.Id, request.PageNumber, request.PageSize);

                if (categories == null || categories.Count == 0)
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

            // TODO: Validation with MediatR?
            private void ValidateRequest(Query request)
            {
                request.PageNumber = request.PageNumber != 0 ? request.PageNumber : 1;
                request.PageSize = request.PageSize != 0 ? request.PageSize : 10;
            }
        }

        public class Response
        {
            public ICollection<CategoryDto> Content { get; set; } = new List<CategoryDto>();
        }
    }
}
