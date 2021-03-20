using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Dtos.User
{
    /// <summary>
    /// Represents a user DTO.
    /// </summary>
    public class UserDto
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
        /// Represents the user role.
        /// </summary>
        public string Role { get; set; }
    }
}
