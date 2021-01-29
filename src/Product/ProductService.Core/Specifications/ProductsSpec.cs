using System;
using System.Linq.Expressions;
using N8T.Core.Specification;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public class ProductsSpec : SpecificationBase<Product>
    {
        private readonly int _quantity;

        public ProductsSpec(int quantity, int page, int pageSize)
        {
            _quantity = quantity;
            AddInclude(x => x.Returns);
            AddInclude(x => x.Code);

            ApplySorting("nameDesc");

            ApplyPaging(page, pageSize);
        }

        public override Expression<Func<Product, bool>> Criteria => p => p.Quantity < _quantity;
    }
}
