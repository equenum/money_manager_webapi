using MoneyManager.Api.Core;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Infrastructure.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.IntegrationTests.Controllers.v1.ReportsController
{
    public class ReportsTestFixtures : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public int CreatedTestCategoryId { get; set; }
        public DateTime TestCategoryDate { get; set; }

        public ReportsTestFixtures()
        {
            _context = new ApplicationDbContext();

            CreateTestCategory();
        }

        public void Dispose()
        {
            DeleteTestCategory(CreatedTestCategoryId);
        }

        private void CreateTestCategory()
        {
            var category = new Category()
            {
                Name = "Test report fixtures",
                Description = "Test category",
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            var transaction = new Transaction()
            {
                Type = TransactionType.Expense,
                Description = "Test report transaction",
                Category = category,
                CategoryId = category.Id,
                Amount = 100,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            CreatedTestCategoryId = category.Id;
            TestCategoryDate = transaction.Created;
        }

        private void DeleteTestCategory(int id)
        {
            var categoryToBeRemoved = _context.Categories.Find(id);

            if (categoryToBeRemoved != null)
            {
                _context.Categories.Remove(categoryToBeRemoved);
                _context.SaveChanges();
            }
        }
    }
}
