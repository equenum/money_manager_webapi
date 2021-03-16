using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        // TODO: Make it async
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
        
        public ICollection<Transaction> GetByCategory(int categoryId)
        {
            return _dbSet.Where(t => t.CategoryId == categoryId)
                         .OrderBy(t => t.Id)
                         .ToList();
        }

        public ICollection<Transaction> GetByDay(DateTime date)
        {
            return _dbSet.Where(t => DbFunctions.TruncateTime(t.Created) == DbFunctions.TruncateTime(date))
                         .OrderBy(t => t.Id)
                         .ToList();
        }

        public ICollection<Transaction> GetByPeriod(DateTime firstDate, DateTime lastDate)
        {
            return _dbSet.Where(t => DbFunctions.TruncateTime(t.Created) >= DbFunctions.TruncateTime(firstDate) && 
                                     DbFunctions.TruncateTime(t.Created) <= DbFunctions.TruncateTime(lastDate))
                         .OrderBy(t => t.Id)
                         .ToList();
        }

        public Transaction GetNewelyCreated(DateTime date, int categoryId, int amount)
        {
            return _dbSet.Where(t => t.CategoryId == categoryId && 
                                     t.Amount == amount && 
                                     t.Created.Year == date.Year && 
                                     t.Created.Month == date.Month && 
                                     t.Created.Day == date.Day && 
                                     t.Created.Hour == date.Hour &&
                                     t.Created.Minute == date.Minute && 
                                     t.Created.Second == date.Second && 
                                     t.Created.Millisecond == date.Millisecond)
                         .FirstOrDefault();
        }
    }
}
