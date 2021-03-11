using MoneyManager.Api.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.EntityConfigurations
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(c => c.Description)
                .HasMaxLength(2000);
        }
    }
}
