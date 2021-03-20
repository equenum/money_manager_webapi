using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Common
{
    /// <summary>
    /// Represents base domain entity.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Represents the entity id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the entity creation time.
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Represents the entity modification time.
        /// </summary>
        public DateTime Modified { get; set; }
    }
}
