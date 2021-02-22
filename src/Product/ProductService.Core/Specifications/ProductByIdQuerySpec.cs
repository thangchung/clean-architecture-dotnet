using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using N8T.Core.Domain;
using N8T.Core.Specification;
using N8T.Infrastructure.App.Dtos;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public sealed class ProductByIdQuerySpec : SpecificationBase<Product>
    {
        private readonly Guid _id;

        public ProductByIdQuerySpec([NotNull] IItemQuery<Guid, ProductDto> queryInput)
        {
            ApplyIncludeList(queryInput.Includes);

            _id = queryInput.Id;
        }

        public override Expression<Func<Product, bool>> Criteria => p => p.Id == _id;
    }
}
