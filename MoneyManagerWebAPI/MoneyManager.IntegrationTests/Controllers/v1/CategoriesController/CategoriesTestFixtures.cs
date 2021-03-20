using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Features.Categories.Commands;
using MoneyManager.Api.Core.Interfaces;
using MoneyManager.Api.Infrastructure.Data.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.IntegrationTests.Controllers.Common.v1
{
    public class CategoriesTestFixtures : IDisposable
    {
        public int CreatedTestCategoryId { get; set; }
        public int CategoryToBeRemovedId { get; set; } = 0;

        private readonly ApplicationDbContext _context;

        public CategoriesTestFixtures()
        {
            _context = new ApplicationDbContext();

            CreateTestCategory();
        }

        public void Dispose()
        {
            if (CreatedTestCategoryId != 0)
            {
                DeleteTestCategory(CreatedTestCategoryId);
            }

            if (CategoryToBeRemovedId != 0)
            {
                DeleteTestCategory(CategoryToBeRemovedId);
            }
        }


        private void CreateTestCategory()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "Test category for integration testing",
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            var createdCategory = _context.Categories.Add(category);
            _context.SaveChanges();

            CreatedTestCategoryId = createdCategory.Id;
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
