using N8T.Core.Domain;
using N8T.Infrastructure.EfCore;

namespace CustomerService.Infrastructure.Data
{
    public class Repository<TEntity> : RepositoryBase<MainDbContext, TEntity> where TEntity : EntityRootBase
    {
        public Repository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
