using MoneyManager.Api.Core.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Interfaces.Repositories
{
    public interface IUserRepositoryAsync
    {
        Task<User> FindAsync(string username);
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(User user);
    }
}
