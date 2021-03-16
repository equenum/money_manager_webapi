using MoneyManager.Api.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Interfaces.Repositories
{
    public interface ITransactionRepositoryAsync : IGenericRepositoryAsync<Transaction>
    {
        Task<ICollection<Transaction>> GetByCategoryAsync(int categoryId);
        Task<ICollection<Transaction>> GetByDateAsync(DateTime date);
        Task<ICollection<Transaction>> GetByPeriodAsync(DateTime beginningDate, DateTime endDate);
        Task<Transaction> GetNewelyCreatedAsync(DateTime date, int categoryId, int amount);
    }
}
