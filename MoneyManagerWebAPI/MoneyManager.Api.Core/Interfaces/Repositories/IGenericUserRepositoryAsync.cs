using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Interfaces.Repositories
{
    public interface IGenericUserRepositoryAsync<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> RegisterAsync(TEntity user);
        Task RemoveAsync(TEntity user);
    }
}
