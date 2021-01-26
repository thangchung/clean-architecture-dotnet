using System;
using System.Linq;
using System.Linq.Expressions;
using N8T.Core.Specification;

namespace ProductService.Core.Domain
{
    public class ProductReturnReasonSpec : SpecificationBase<Product>
    {
        private readonly ReturnReason _returnReason;

        public ProductReturnReasonSpec(ReturnReason returnReason)
        {
            _returnReason = returnReason;
        }

        public override Expression<Func<Product, bool>> SpecExpression
        {
            get
            {
                return product => product.Returns.ToList().Exists(returns => returns.Reason == _returnReason);
            }
        }
    }
}
