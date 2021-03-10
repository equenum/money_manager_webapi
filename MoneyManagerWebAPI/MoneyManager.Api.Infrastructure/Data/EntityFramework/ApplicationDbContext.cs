using Microsoft.Extensions.Configuration;
using MoneyManager.Api.Infrastructure.Data.Entities;
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
            // this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // TODO: Extract the configurations to separate classes.

            // Transaction
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.CategoryId)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Type)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Description)
                .HasMaxLength(2000);

            // Category
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(2000);
        }
    }
}
