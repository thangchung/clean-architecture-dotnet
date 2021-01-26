using System;
using System.Collections.Generic;
using N8T.Core.Domain;
using N8T.Core.Specification;

namespace N8T.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        TEntity FindById(Guid id);
        TEntity FindOne(ISpecification<TEntity> spec);
        IEnumerable<TEntity> Find(ISpecification<TEntity> spec);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
