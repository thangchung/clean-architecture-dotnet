using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N8T.Core.Domain;
using N8T.Core.Repository;

namespace N8T.Infrastructure.EfCore
{
    public interface IExRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, IAggregateRoot
    {
        Task<TEntity> FindOneAsync(IExSpecification<TEntity> spec);
        Task<List<TEntity>> FindAsync(IExSpecification<TEntity> spec);
    }

    public class RepositoryBase<TDbContext, TEntity> : IExRepository<TEntity>
        where TEntity : EntityBase, IAggregateRoot
        where TDbContext : DbContext
    {
        private readonly IDbContextFactory<TDbContext> _dbContextFactory;

        protected RepositoryBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public TEntity FindById(Guid id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return dbContext.Set<TEntity>().SingleOrDefault(e => e.Id == id);
        }

        public Task<TEntity> FindOneAsync(IExSpecification<TEntity> spec)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return dbContext.Set<TEntity>().Where(spec.SpecExpression).SingleOrDefaultAsync();
        }

        public Task<List<TEntity>> FindAsync(IExSpecification<TEntity> spec)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            return dbContext.Set<TEntity>().Where(spec.SpecExpression).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            await dbContext.Set<TEntity>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public Task RemoveAsync(TEntity entity)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            dbContext.Set<TEntity>().Remove(entity);

            return dbContext.SaveChangesAsync();
        }
    }
}
