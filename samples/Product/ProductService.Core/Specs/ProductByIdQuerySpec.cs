using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using N8T.Core.Domain;
using N8T.Core.Specification;
using ProductService.Core.Entities;

namespace ProductService.Core.Specs
{
    public sealed class ProductByIdQuerySpec<TResponse> : SpecificationBase<Product>
    {
        private readonly Guid _id;

        public ProductByIdQuerySpec([NotNull] IItemQuery<Guid, TResponse> queryInput)
        {
            ApplyIncludeList(queryInput.Includes);

            _id = queryInput.Id;
        }

        public override Expression<Func<Product, bool>> Criteria => p => p.Id == _id;
    }
}
