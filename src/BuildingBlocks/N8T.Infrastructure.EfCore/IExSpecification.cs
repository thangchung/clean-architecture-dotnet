using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using N8T.Core.Specification;

namespace N8T.Infrastructure.EfCore
{
    public interface IExSpecification<T> : ISpecification<T>
    {
        List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }
    }
}
