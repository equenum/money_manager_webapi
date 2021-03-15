using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Core.Domain.Entities.Authentication;
using MoneyManager.Api.Infrastructure.Data.EntityFramework.EntityConfigurations;
using MoneyManager.Api.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext()
            : base(AppConfigHelper.GetConnectionString())
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new TransactionConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
