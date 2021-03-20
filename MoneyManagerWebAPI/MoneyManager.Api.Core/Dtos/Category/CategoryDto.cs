using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Dtos.Category
{
    /// <summary>
    /// Represents a transaction category DTO.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Represents the transaction id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the category name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Represents the category's transactions.
        /// </summary>
        public string Description { get; set; }
    }
}
