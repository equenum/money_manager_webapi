using MoneyManager.Api.Core;
using MoneyManager.Api.Core.Domain.Common;
using MoneyManager.Api.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    public class Transaction : BaseEntity, ITransaction
    {
        public TransactionType Type { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        [Range(1, 20000)]
        public int Amount { get; set; }
    }
}
