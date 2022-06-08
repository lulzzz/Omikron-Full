using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Omikron.SharedKernel.Infrastructure.Data.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly DbContext _repositoryContext;

        protected RepositoryBase(DbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IQueryable<TEntity> FindAll()
        {
            return _repositoryContext.Set<TEntity>();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _repositoryContext.Set<TEntity>()
                .Where(expression);
        }

        public void Create(TEntity entity)
        {
            _repositoryContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _repositoryContext.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _repositoryContext.Set<TEntity>().UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _repositoryContext.Set<TEntity>().Remove(entity);
        }


        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _repositoryContext.Set<TEntity>().RemoveRange(entities);
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _repositoryContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _repositoryContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }
    }
}