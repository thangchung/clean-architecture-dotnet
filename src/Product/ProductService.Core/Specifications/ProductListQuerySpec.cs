using CoolStore.AppContracts.Dtos;
using N8T.Core.Domain;
using N8T.Core.Specification;
using N8T.Infrastructure;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public sealed class ProductListQuerySpec : GridSpecificationBase<Product>
    {
        public ProductListQuerySpec(IListQuery<ListResponseModel<ProductDto>> gridQueryInput)
        {
            ApplyIncludeList(gridQueryInput.Includes);

            ApplyFilterList(gridQueryInput.Filters);

            ApplySortingList(gridQueryInput.Sorts);

            ApplyPaging(gridQueryInput.Page, gridQueryInput.PageSize);
        }
    }
}
