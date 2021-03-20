using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Dtos.User
{
    /// <summary>
    /// Represents an authenticated user DTO.
    /// </summary>
    public class UserAuthDto
    {
        /// <summary>
        /// Represents the authenticated user id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the authenticated user username.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Represents the authenticated user role.
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Represents the authenticated user JWT-token.
        /// </summary>
        public string Token { get; set; }
    }
}
