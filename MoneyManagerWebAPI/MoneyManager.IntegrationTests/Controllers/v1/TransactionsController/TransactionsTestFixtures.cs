using MoneyManager.Api.Core;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Infrastructure.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyManager.IntegrationTests.Controllers.v1.TransactionsController
{
    public class TransactionsTestFixtures : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public int CreatedTestTransactionId { get; set; }
        public int TransactionToBeRemovedId { get; set; } = 0;
        public int CreatedTestCategoryId { get; set; }

        public TransactionsTestFixtures()
        {
            _context = new ApplicationDbContext();

            CreateTestCategory();
        }

        public void Dispose()
        {
            if (TransactionToBeRemovedId != 0)
            {
                DeleteTestTransaction(TransactionToBeRemovedId);
            }

            DeleteTestCategory();
        }

        private void CreateTestCategory()
        {
            var category = new Category()
            {
                Name = "Test category fixtures",
                Description = "Test category",
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            var transaction = new Transaction()
            {
                Type = TransactionType.Expense,
                Description = "Test transaction",
                Category = category,
                CategoryId = category.Id,
                Amount = 100,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            _context.Transactions.Add(transaction);

            var transactions = new List<Transaction>() { transaction };
            category.Transactions = transactions;

            _context.SaveChanges();
            
            var createdTransactions = new List<Transaction>();

            foreach (var trans in category.Transactions)
            {
                createdTransactions.Add(trans);
            }

            var testTransaction = createdTransactions[0];
            
            CreatedTestTransactionId = testTransaction.Id;
            CreatedTestCategoryId = category.Id;
        }

        private void DeleteTestCategory()
        {
            var categoryToBeRemoved = _context.Categories.Find(CreatedTestCategoryId);

            if (categoryToBeRemoved != null)
            {
                var transactionsToBeDeleted = new List<Transaction>();

                if (categoryToBeRemoved.Transactions.Count() != 0)
                {
                    foreach (var transaction in categoryToBeRemoved.Transactions)
                    {
                        transactionsToBeDeleted.Add(transaction);
                    }
                }

                if (transactionsToBeDeleted.Count != 0)
                {
                    foreach (var transaction in transactionsToBeDeleted)
                    {
                        DeleteTestTransaction(transaction.Id);
                    }
                }
            }

            _context.Categories.Remove(categoryToBeRemoved);
            _context.SaveChanges();
        }

        private void DeleteTestTransaction(int id)
        {
            var transactionToBeRemoved = _context.Transactions.Find(id);

            if (transactionToBeRemoved != null)
            {
                _context.Transactions.Remove(transactionToBeRemoved);
                _context.SaveChanges();
            }
        }
    }
}
