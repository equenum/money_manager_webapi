using MoneyManager.Api.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    /// <summary>
    /// Represents a transactionn report.
    /// </summary>
    public class TransactionReport : ITransactionReport
    {
        /// <summary>
        /// Represenst a list of report transactions.
        /// </summary>
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        /// <summary>
        /// Represents report total value.
        /// </summary>
        public int Total { get; set; }
    }
}
