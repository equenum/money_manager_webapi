using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepositoryAsync Categories { get; }
        ITransactionRepositoryAsync Transactions { get; }
        IUserRepositoryAsync Users { get; }
        int Complete();
    }
}
