using Microsoft.Extensions.Configuration;
using MoneyManager.Api.Core.Domain.Entities;
using MoneyManager.Api.Infrastructure.Data.EntityFramework.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationDbContext() 
            : base(AppConfig.GetConnectionString())
        {
            // TODO: Setup explicit loading 
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new TransactionConfiguration());
        }
    }
}
