using N8T.Core.Domain;
using N8T.Core.Specification;
using ProductService.CoreApp.Entities;

namespace ProductService.CoreApp.Specs
{
    public sealed class ProductListQuerySpec<TResponse> : GridSpecificationBase<Product>
    {
        public ProductListQuerySpec(IListQuery<ListResponseModel<TResponse>> gridQueryInput)
        {
            ApplyIncludeList(gridQueryInput.Includes);

            ApplyFilterList(gridQueryInput.Filters);

            ApplySortingList(gridQueryInput.Sorts);

            ApplyPaging(gridQueryInput.Page, gridQueryInput.PageSize);
        }
    }
}
