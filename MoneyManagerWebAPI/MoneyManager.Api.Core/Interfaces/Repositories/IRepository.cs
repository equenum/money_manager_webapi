using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoneyManager.Api.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        ICollection<TEntity> GetAll();
        ICollection<TEntity> GetAllPaged(Expression<Func<TEntity, int>> predicate, int pageNumber, int pageSize);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(ICollection<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(ICollection<TEntity> entities);
    }
}
