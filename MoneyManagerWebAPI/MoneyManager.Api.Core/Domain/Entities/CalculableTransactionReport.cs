using MoneyManager.Api.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    public class CalculableTransactionReport : ITransactionReport
    {
        private readonly ITransactionReport _decoratedReport;
        
        public int Total 
        { 
            get => _decoratedReport.Total; 
            set => _decoratedReport.Total = value; 
        }

        public ICollection<Transaction> Transactions 
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
