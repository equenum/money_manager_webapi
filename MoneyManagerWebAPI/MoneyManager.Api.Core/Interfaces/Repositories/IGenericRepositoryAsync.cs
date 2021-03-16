using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Api.Core.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, int>> predicate, int pageNumber, int pageSize);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);
        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(ICollection<TEntity> entities);
    }
}
