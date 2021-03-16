using MoneyManager.Api.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Interfaces.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        ICollection<Transaction> GetByCategory(int categoryId);
        ICollection<Transaction> GetByDay(DateTime date);
        ICollection<Transaction> GetByPeriod(DateTime firstDate, DateTime lastDay);
        Transaction GetNewelyCreated(DateTime date, int categoryId, int amount);
    }
}
