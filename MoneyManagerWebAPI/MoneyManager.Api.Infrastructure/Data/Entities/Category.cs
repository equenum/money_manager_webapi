using MoneyManager.Api.Infrastructure.Data.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
