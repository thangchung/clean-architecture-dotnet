using N8T.Core.Domain;
using N8T.Infrastructure.EfCore;

namespace ProductService.Infrastructure.Data
{
    public class Repository<TEntity> : RepositoryBase<MainDbContext, TEntity> where TEntity : EntityBase, IAggregateRoot
    {
        public Repository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
