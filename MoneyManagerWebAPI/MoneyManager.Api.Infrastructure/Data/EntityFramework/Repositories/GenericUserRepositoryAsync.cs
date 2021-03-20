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
    public class GenericUserRepositoryAsync<TEntity> : IGenericUserRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericUserRepositoryAsync(DbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> RegisterAsync(TEntity user)
        {
            _dbSet.Add(user);
            await Context.SaveChangesAsync();

            return user;
        }

        public async Task RemoveAsync(TEntity user)
        {
            _dbSet.Remove(user);
            await Context.SaveChangesAsync();
        }
    }
}
