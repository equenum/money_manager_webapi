using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public ICollection<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public ICollection<TEntity> GetAllPaged(Expression<Func<TEntity, int>> predicate, int pageNumber, int pageSize)
        {
            return _dbSet.OrderBy(predicate)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .AsNoTracking()
                         .ToList();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = _dbSet.Where(predicate).FirstOrDefault();
            
            return entity;
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            Context.SaveChanges();
        }

        public void AddRange(ICollection<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            Context.SaveChanges();
        }
        
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            Context.SaveChanges();
        }

        public void RemoveRange(ICollection<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            Context.SaveChanges();
        }
    }
}
