using MoneyManager.Api.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.EntityConfigurations
{
    public class TransactionConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration()
        {
            Property(t => t.Amount)
                .IsRequired();

            Property(t => t.CategoryId)
                .IsRequired();

            Property(t => t.Type)
                .IsRequired();

            Property(t => t.Description)
                .HasMaxLength(2000);
        }
    }
}
