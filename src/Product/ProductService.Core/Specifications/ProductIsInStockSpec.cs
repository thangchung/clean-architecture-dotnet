using System;
using System.Linq.Expressions;
using N8T.Core.Specification;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public class ProductIsInStockSpec : SpecificationBase<Product>
    {
        private readonly Guid _productId;
        private readonly int _quantity;

        public ProductIsInStockSpec(Guid productId, int quantity)
        {
            _productId = productId;
            _quantity = quantity;
        }

        public override Expression<Func<Product, bool>> SpecExpression
        {
            get
            {
                return product => product.Id == _productId && product.Active
                                                           && product.Quantity >= _quantity;
            }
        }
    }
}
