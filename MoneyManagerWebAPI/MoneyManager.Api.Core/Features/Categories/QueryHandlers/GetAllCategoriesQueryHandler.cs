using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Features.Categories.Queries;
using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Features.Categories.QueryHandlers
{
    public class GetAllCategoriesQueryHandler
    {
        private readonly ICategoryRepository _categoryRepository;
        // TODO: add mapper?

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> Handle(GetAllCategoriesQuery query)
        {
            return _categoryRepository.GetAllPaged(query.PageNumber, query.PageSize);
        }
    }
}
