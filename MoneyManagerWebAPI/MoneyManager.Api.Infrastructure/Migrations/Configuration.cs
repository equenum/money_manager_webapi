namespace MoneyManager.Api.Infrastructure.Migrations
{
    using MoneyManager.Api.Core;
    using MoneyManager.Api.Core.Domain.Entities;
    using System;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoneyManager.Api.Infrastructure.Data.EntityFramework.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoneyManager.Api.Infrastructure.Data.EntityFramework.ApplicationDbContext context)
        {
            // Seed Category table
            #region SeedFoodExpensesCategory
            var foodExpensesCategory = new Category
            {
                Name = "Food",
                Description = "Food expenses",
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            var foodExpensesTransactions = new Collection<Transaction>()
            {
                new Transaction() 
                {  
                    Amount = 100, 
                    Category = foodExpensesCategory, 
                    CategoryId = foodExpensesCategory.Id, 
                    Type = TransactionType.Expense, 
                    Description = "Apples", 
                    Created = DateTime.Now, 
                    Modified = DateTime.Now  
                },
                new Transaction() 
                {
                    Amount = 250,
                    Category = foodExpensesCategory,
                    CategoryId = foodExpensesCategory.Id,
                    Type = TransactionType.Expense,
                    Description = "Grapes",
                    Created = DateTime.Now.AddMonths(-1),
                    Modified = DateTime.Now.AddMonths(-1)
                },
                new Transaction() 
                {
                    Amount = 50,
                    Category = foodExpensesCategory,
                    CategoryId = foodExpensesCategory.Id,
                    Type = TransactionType.Expense,
                    Description = "Juice",
                    Created = DateTime.Now.AddMonths(-2),
                    Modified = DateTime.Now.AddMonths(-2)
                }
            };

            foodExpensesCategory.Transactions = foodExpensesTransactions;

            context.Categories.AddOrUpdate(c => c.Name, foodExpensesCategory);
            #endregion

            #region SeedPayChequeCategory
            var payChequeCategory = new Category
            {
                Name = "Pay cheque",
                Description = "Pay cheque income",
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            var payChequeTransactions = new Collection<Transaction>()
            {
                new Transaction() 
                {  
                    Amount = 1000, 
                    Category = payChequeCategory, 
                    CategoryId = payChequeCategory.Id, 
                    Type = TransactionType.Income, 
                    Description = "Pay cheque for month 1", 
                    Created = DateTime.Now, 
                    Modified = DateTime.Now  
                },
                new Transaction() 
                {
                    Amount = 2000,
                    Category = payChequeCategory,
                    CategoryId = payChequeCategory.Id,
                    Type = TransactionType.Income,
                    Description = "Pay cheque for month 2",
                    Created = DateTime.Now.AddMonths(-1),
                    Modified = DateTime.Now.AddMonths(-1)
                },
                new Transaction() 
                {
                    Amount = 3000,
                    Category = payChequeCategory,
                    CategoryId = payChequeCategory.Id,
                    Type = TransactionType.Income,
                    Description = "Pay cheque for month 3",
                    Created = DateTime.Now.AddMonths(-2),
                    Modified = DateTime.Now.AddMonths(-2)
                }
            };

            payChequeCategory.Transactions = payChequeTransactions;

            context.Categories.AddOrUpdate(c => c.Name, payChequeCategory);
            #endregion
        }
    }
}
