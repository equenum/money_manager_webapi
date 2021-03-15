using Microsoft.Extensions.Options;
using MoneyManager.Api.Core.Domain.Settings;
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
        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IOptions<SecretSettings> secretSettings)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Transactions = new TransactionRepository(_context);
            Users = new UserRepository(_context, secretSettings);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
