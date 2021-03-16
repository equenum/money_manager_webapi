using MoneyManager.Api.Core.Domain.Entities;
using System;

namespace MoneyManager.Api.Core.Interfaces.Entities
{
    public interface ITransaction
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        int Amount { get; set; }
        Category Category { get; set; }
        int CategoryId { get; set; }
        string Description { get; set; }
        TransactionType Type { get; set; }
    }
}