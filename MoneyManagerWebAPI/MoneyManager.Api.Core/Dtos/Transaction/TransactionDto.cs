using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Dtos.Transaction
{
    /// <summary>
    /// Represents a financial transaction DTO.
    /// </summary>
    public class TransactionDto
    {
        /// <summary>
        /// Represents the transaction id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the transaction type.
        /// </summary>
        public TransactionType Type { get; set; }
        /// <summary>
        /// Represents the transaction description.
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Represents the transaction description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Represents the transaction amount.
        /// </summary>
        public int Amount { get; set; }
    }
}
