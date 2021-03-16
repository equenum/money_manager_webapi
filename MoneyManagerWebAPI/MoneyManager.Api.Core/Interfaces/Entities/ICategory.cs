using MoneyManager.Api.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MoneyManager.Api.Core.Interfaces.Entities
{
    public interface ICategory
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        IEnumerable<Transaction> Transactions { get; set; }
    }
}