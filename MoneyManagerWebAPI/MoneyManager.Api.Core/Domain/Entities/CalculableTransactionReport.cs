using MoneyManager.Api.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    /// <summary>
    /// Represents a transaction decorator. Adds the ability to calculate transaction total. 
    /// </summary>
    public class CalculableTransactionReport : ITransactionReport
    {
        private readonly ITransactionReport _decoratedReport;
        
        public int Total 
        { 
            get => _decoratedReport.Total; 
            set => _decoratedReport.Total = value; 
        }

        public List<Transaction> Transactions 
        { 
            get => _decoratedReport.Transactions; 
            set => _decoratedReport.Transactions = value; 
        }

        public CalculableTransactionReport(ITransactionReport transactionReport)
        {
            _decoratedReport = transactionReport;
        }

        public int CalculateTotal()
        {
            Total = 0;

            if (Transactions == null)
            {
                return 0;
            }

            foreach (var transaction in Transactions)
            {
                if (transaction.Type == TransactionType.Expense)
                {
                    Total -= transaction.Amount;
                }
                else
                {
                    Total += transaction.Amount;
                }
            }

            return Total;
        }
    }
}
