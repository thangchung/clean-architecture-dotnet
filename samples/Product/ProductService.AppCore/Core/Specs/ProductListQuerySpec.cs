using N8T.Core.Domain;
using N8T.Core.Specification;

namespace ProductService.AppCore.Core.Specs
{
    public sealed class ProductListQuerySpec<TResponse> : GridSpecificationBase<Product>
    {
        public ProductListQuerySpec(IListQuery<ListResultModel<TResponse>> gridQueryInput)
        {
            ApplyIncludeList(gridQueryInput.Includes);

            ApplyFilterList(gridQueryInput.Filters);

            ApplySortingList(gridQueryInput.Sorts);

            ApplyPaging(gridQueryInput.Page, gridQueryInput.PageSize);
        }
    }
}
