using MoneyManager.Api.Core.Interfaces.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Domain.Entities.Authentication
{
    /// <summary>
    /// Represents an API user.
    /// </summary>
    public class User : IUser
    {
        /// <summary>
        /// Represents the user id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the user username.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Represents the user password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Represents the user role.
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Represents the user token.
        /// </summary>
        public string Token { get; set; }
    }
}
