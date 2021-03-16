namespace MoneyManager.Api.Infrastructure.Migrations
{
    using MoneyManager.Api.Core;
    using MoneyManager.Api.Core.Domain.Entities;
    using MoneyManager.Api.Core.Domain.Entities.Authentication;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.EntityFramework.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.EntityFramework.ApplicationDbContext context)
        {
            #region SeedCategoriesTable

            #region SeedFoodExpensesCategory

            var foodExpensesCategory = new Category
            {
                Name = "Food",
                Description = "Food expenses",
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

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

            context.Categories.AddOrUpdate(c => c.Name, payChequeCategory);

            #endregion

            #endregion

            #region SeedTransactionsTable

            #region SeedFoodExpensesTransactions

            var foodExpensesTransactions = new List<Transaction>()
            {
                new Transaction() 
                {  
                    Amount = 100, 
                    Category = foodExpensesCategory,
                    CategoryId = foodExpensesCategory.Id, 
                    Type = TransactionType.Expense, 
                    Description = "Apples (Test)", 
                    Created = DateTime.Now, 
                    Modified = DateTime.Now  
                },
                new Transaction()
                {
                    Amount = 1050,
                    Category = foodExpensesCategory,
                    CategoryId = foodExpensesCategory.Id,
                    Type = TransactionType.Expense,
                    Description = "Apple jam (Test)",
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                },
                new Transaction() 
                {
                    Amount = 250,
                    Category = foodExpensesCategory,
                    CategoryId = foodExpensesCategory.Id,
                    Type = TransactionType.Expense,
                    Description = "Grapes (Test)",
                    Created = DateTime.Now.AddMonths(-1),
                    Modified = DateTime.Now.AddMonths(-1)
                },
                new Transaction()
                {
                    Amount = 150,
                    Category = foodExpensesCategory,
                    CategoryId = foodExpensesCategory.Id,
                    Type = TransactionType.Expense,
                    Description = "Grape juice (Test)",
                    Created = DateTime.Now.AddMonths(-1),
                    Modified = DateTime.Now.AddMonths(-1)
                },
                new Transaction() 
                {
                    Amount = 50,
                    Category = foodExpensesCategory,
                    CategoryId = foodExpensesCategory.Id,
                    Type = TransactionType.Expense,
                    Description = "Juice (Test)",
                    Created = DateTime.Now.AddMonths(-2),
                    Modified = DateTime.Now.AddMonths(-2)
                },
                new Transaction()
                {
                    Amount = 450,
                    Category = foodExpensesCategory,
                    CategoryId = foodExpensesCategory.Id,
                    Type = TransactionType.Expense,
                    Description = "Ice cream (Test)",
                    Created = DateTime.Now.AddMonths(-2),
                    Modified = DateTime.Now.AddMonths(-2)
                }
            };

            context.Transactions.AddOrUpdate(t => t.Description, foodExpensesTransactions.ToArray());

            #endregion

            #region SeedChequeIncomeTransactions
            var payChequeTransactions = new List<Transaction>()
            {
                new Transaction() 
                {  
                    Amount = 1000, 
                    Category = payChequeCategory,
                    CategoryId = payChequeCategory.Id, 
                    Type = TransactionType.Income, 
                    Description = "Pay cheque for month 1 (Test)", 
                    Created = DateTime.Now, 
                    Modified = DateTime.Now  
                },
                new Transaction() 
                {
                    Amount = 2000,
                    Category = payChequeCategory,
                    CategoryId = payChequeCategory.Id,
                    Type = TransactionType.Income,
                    Description = "Pay cheque for month 2 (Test)",
                    Created = DateTime.Now.AddMonths(-1),
                    Modified = DateTime.Now.AddMonths(-1)
                },
                new Transaction() 
                {
                    Amount = 3000,
                    Category = payChequeCategory,
                    CategoryId = payChequeCategory.Id,
                    Type = TransactionType.Income,
                    Description = "Pay cheque for month 3 (Test)",
                    Created = DateTime.Now.AddMonths(-2),
                    Modified = DateTime.Now.AddMonths(-2)
                }
            };

            context.Transactions.AddOrUpdate(t => t.Description, payChequeTransactions.ToArray());

            #endregion

            #endregion

            #region SeedUsersTable

            var users = new List<User>() 
            { 
                new User()
                {
                    Username = "regular",
                    Password = "regular",
                    Role = "USER"
                },
                new User()
                {
                    Username = "admin",
                    Password = "admin",
                    Role = "ADMIN"
                },
                new User()
                {
                    Username = "superadmin",
                    Password = "superadmin",
                    Role = "SUPERADMIN"
                }
            };

            foreach (var user in users)
            {
                context.Users.AddOrUpdate(u => u.Username, user);
            }
            
            #endregion
        }
    }
}
