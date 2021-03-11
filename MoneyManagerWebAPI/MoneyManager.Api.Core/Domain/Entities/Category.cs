using MoneyManager.Api.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
