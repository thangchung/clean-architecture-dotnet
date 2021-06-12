using System;
using System.Linq;
using System.Linq.Expressions;
using N8T.Core.Specification;

namespace ProductService.AppCore.Core.Specs
{
    public class ProductReturnReasonSpec : SpecificationBase<Product>
    {
        private readonly ReturnReason _returnReason;

        public ProductReturnReasonSpec(ReturnReason returnReason)
        {
            _returnReason = returnReason;
        }

        public override Expression<Func<Product, bool>> Criteria =>
            product => product.Returns.ToList().Exists(returns => returns.Reason == _returnReason);
    }
}
