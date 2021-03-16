using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Dtos.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
