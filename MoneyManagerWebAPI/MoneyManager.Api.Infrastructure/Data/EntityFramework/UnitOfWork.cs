using MoneyManager.Api.Core.Interfaces;
using MoneyManager.Api.Core.Interfaces.Repositories;
using MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICategoryRepository Categories { get; private set; }
        public ITransactionRepository Transactions { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Transactions = new TransactionRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
