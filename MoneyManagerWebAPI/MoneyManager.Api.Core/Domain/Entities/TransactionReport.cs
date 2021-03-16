using MoneyManager.Api.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    public class TransactionReport : ITransactionReport
    {
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public int Total { get; set; }

        public TransactionReport(ICollection<Transaction> transactions)
        {
            Transactions = transactions;
        }
    }
}
