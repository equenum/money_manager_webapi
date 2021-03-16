using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    public class TransactionReport
    {
        // TODO: Implement decorator
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public int Total { get; set; }

        public TransactionReport(ICollection<Transaction> transactions)
        {
            Transactions = transactions;
        }

        public int CalculateTotal()
        {
            Total = 0;

            foreach (var transaction in Transactions)
            {
                if (transaction.Type == TransactionType.Expense)
                {
                    Total = Total - transaction.Amount;
                }
                else
                {
                    Total = Total + transaction.Amount;
                }
            }

            return Total;
        }
    }
}
