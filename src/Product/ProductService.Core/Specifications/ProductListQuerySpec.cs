using N8T.Core.Domain;
using N8T.Core.Specification;
using N8T.Infrastructure.LambdaExpression;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public sealed class ProductListQuerySpec : GridSpecificationBase<Product>
    {
        public ProductListQuerySpec(IListQueryModel gridQueryModel)
        {
            foreach (var include in gridQueryModel.Includes)
            {
                AddInclude(include);
            }

            foreach (var (fieldName, comparision, fieldValue) in gridQueryModel.Filters)
            {
                Criterias.Add(PredicateBuilder.Build<Product>(fieldName, comparision, fieldValue));
            }

            foreach (var sort in gridQueryModel.Sorts)
            {
                ApplySorting(sort);
            }

            ApplyPaging(gridQueryModel.Page, gridQueryModel.PageSize);
        }
    }
}
