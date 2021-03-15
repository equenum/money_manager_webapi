using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Api.Core.Dtos.User
{
    public class UserAuthDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
