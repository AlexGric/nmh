using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected BaseRepository(TaskManagerContext context)
        {
            TaskManagerContext = context;
        }
        private DbSet<TEntity> _entities;
        protected TaskManagerContext TaskManagerContext;
        protected DbSet<TEntity> Entities => this._entities ??= TaskManagerContext.Set<TEntity>();
        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync().ConfigureAwait(false);
        }
        public virtual async Task<IReadOnlyCollection<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicat)
        {
            return await Entities.Where(predicat).ToListAsync().ConfigureAwait(false);
        }
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await Entities.AddAsync(entity).ConfigureAwait(false);
            return entity;
        }
    }
}
