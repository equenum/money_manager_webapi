using MoneyManager.Api.Core.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoneyManager.Api.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User Find(string username);
        User Authenticate(string username, string password);
        void Register(string username, string password, string role);
    }
}
