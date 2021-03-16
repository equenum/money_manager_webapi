using MoneyManager.Api.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, int>> predicate, int pageNumber, int pageSize)
        {
            return await _dbSet.OrderBy(predicate)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .AsNoTracking()
                         .ToListAsync();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            await Context.SaveChangesAsync();

            return entities;
        }
        
        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(ICollection<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await Context.SaveChangesAsync();
        }
    }
}
