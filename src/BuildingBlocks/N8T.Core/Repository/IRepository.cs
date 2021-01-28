using System;
using System.Threading.Tasks;
using N8T.Core.Domain;

namespace N8T.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : EntityBase, IAggregateRoot
    {
        TEntity FindById(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
