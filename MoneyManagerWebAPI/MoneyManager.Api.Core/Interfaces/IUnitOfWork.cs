using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        ITransactionRepository Transactions { get; }
        int Complete();
    }
}
