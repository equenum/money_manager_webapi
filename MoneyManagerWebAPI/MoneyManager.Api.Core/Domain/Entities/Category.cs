using MoneyManager.Api.Core.Domain.Common;
using MoneyManager.Api.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities
{
    /// <summary>
    /// Represents a financial transaction category. 
    /// </summary>
    public class Category : BaseEntity, ICategory
    {
        /// <summary>
        /// Represents the category name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Represents the category description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Represents the category's transactions.
        /// </summary>
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
