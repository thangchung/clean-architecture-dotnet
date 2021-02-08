using System;
using System.Linq.Expressions;
using N8T.Core.Domain;
using N8T.Core.Specification;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public sealed class ProductByIdQuerySpec : SpecificationBase<Product>
    {
        private readonly IItemQueryModel<Guid> _queryModel;

        public ProductByIdQuerySpec(IItemQueryModel<Guid> queryModel)
        {
            _queryModel = queryModel ?? throw new ArgumentNullException(nameof(queryModel));
            foreach (var include in queryModel.Includes)
            {
                AddInclude(include);
            }
        }

        public override Expression<Func<Product, bool>> Criteria => p => p.Id == _queryModel.Id;
    }
}
