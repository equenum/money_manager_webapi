using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public TransactionRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
}
