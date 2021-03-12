using AutoMapper;
using MediatR;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Features.Categories.Commands
{
    public static class CreateCategory
    {
        public class Command : IRequest<Unit>
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public Handler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                _categoryRepository = categoryRepository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = _mapper.Map<Category>(request); // TODO: Wire up created and modified time for repository
                category.Created = DateTime.Now;
                category.Modified = DateTime.Now;

                _categoryRepository.Add(category);

                return Unit.Value;
            }
        }
    }
}
