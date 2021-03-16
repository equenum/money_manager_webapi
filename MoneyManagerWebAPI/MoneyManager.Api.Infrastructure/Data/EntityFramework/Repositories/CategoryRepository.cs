using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepositoryAsync
    {
        public ApplicationDbContext ApplicationDbContext 
        {
            get { return Context as ApplicationDbContext; } 
        }

        public CategoryRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
}
