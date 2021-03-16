using MoneyManager.Api.Core.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username)
                .HasMaxLength(20)
                .IsRequired();

            Property(u => u.Password)
                .HasMaxLength(20)
                .IsRequired();

            Property(u => u.Role)
                .HasMaxLength(20)
                .IsRequired();

            Ignore(u => u.Token);
        }
    }
}
