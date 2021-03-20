using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Settings
{
    /// <summary>
    /// Represents secret settings for JWT-tiken generation.
    /// </summary>
    public class SecretSettings
    {
        /// <summary>
        /// Represents a secret key.
        /// </summary>
        public string Secret { get; set; }
    }
}
