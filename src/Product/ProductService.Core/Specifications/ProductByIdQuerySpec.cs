using System;
using System.Linq.Expressions;
using N8T.Core.Domain;
using N8T.Core.Specification;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public sealed class ProductByIdQuerySpec : SpecificationBase<Product>
    {
        private readonly IItemQueryInput<Guid> _queryInput;

        public ProductByIdQuerySpec(IItemQueryInput<Guid> queryInput)
        {
            _queryInput = queryInput ?? throw new ArgumentNullException(nameof(queryInput));
            foreach (var include in queryInput.Includes)
            {
                AddInclude(include);
            }
        }

        public override Expression<Func<Product, bool>> Criteria => p => p.Id == _queryInput.Id;
    }
}
