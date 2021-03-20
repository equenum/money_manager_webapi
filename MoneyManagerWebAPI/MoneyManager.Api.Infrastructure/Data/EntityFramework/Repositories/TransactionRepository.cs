using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepositoryAsync
    {
        private readonly DbSet<Transaction> _dbSet;

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public TransactionRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
            _dbSet = ApplicationDbContext.Transactions;
        }
        
        public async Task<ICollection<Transaction>> GetByCategoryAsync(int categoryId)
        {
            return await _dbSet.Where(t => t.CategoryId == categoryId)
                         .OrderBy(t => t.Id)
                         .ToListAsync();
        }

        public async Task<ICollection<Transaction>> GetByDateAsync(DateTime date)
        {
            return await _dbSet.Where(t => DbFunctions.TruncateTime(t.Created) == DbFunctions.TruncateTime(date))
                         .OrderBy(t => t.Id)
                         .ToListAsync();
        }

        public async Task<ICollection<Transaction>> GetByPeriodAsync(DateTime firstDate, DateTime lastDate)
        {
            return await _dbSet.Where(t => DbFunctions.TruncateTime(t.Created) >= DbFunctions.TruncateTime(firstDate) && 
                                     DbFunctions.TruncateTime(t.Created) <= DbFunctions.TruncateTime(lastDate))
                         .OrderBy(t => t.Id)
                         .ToListAsync();
        }
    }
}
