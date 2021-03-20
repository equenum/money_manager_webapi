using MoneyManager.Api.Core;
using MoneyManager.Api.Core.Domain.Common;
using MoneyManager.Api.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    /// <summary>
    /// Represents a financial transaction.
    /// </summary>
    public class Transaction : BaseEntity, ITransaction
    {
        /// <summary>
        /// Represents the transaction type.
        /// </summary>
        public TransactionType Type { get; set; }
        /// <summary>
        /// Represents the transaction category (navigation property).
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// Represents the transaction category id.
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Represents the transaction description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Represents the transaction amount.
        /// </summary>
        [Range(1, 20000)]
        public int Amount { get; set; }
    }
}
