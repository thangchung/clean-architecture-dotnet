using N8T.Core.Domain;
using N8T.Core.Specification;
using N8T.Infrastructure.LambdaExpression;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public sealed class ProductListQuerySpec : GridSpecificationBase<Product>
    {
        public ProductListQuerySpec(IListQueryInput gridQueryInput)
        {
            foreach (var include in gridQueryInput.Includes)
            {
                AddInclude(include);
            }

            foreach (var (fieldName, comparision, fieldValue) in gridQueryInput.Filters)
            {
                Criterias.Add(PredicateBuilder.Build<Product>(fieldName, comparision, fieldValue));
            }

            foreach (var sort in gridQueryInput.Sorts)
            {
                ApplySorting(sort);
            }

            ApplyPaging(gridQueryInput.Page, gridQueryInput.PageSize);
        }
    }
}
