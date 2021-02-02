using System.Collections.Generic;
using N8T.Core.Specification;
using N8T.Infrastructure.LambdaExpression;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public class GridQueryModel
    {
        public List<FilterModel> Filters { get; init; } = new();
        public List<string> Sorts { get; init; } = new();
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }

    public class FilterModel
    {
        public string FieldName { get; init; } = string.Empty;
        public string Comparision { get; init; } = string.Empty;
        public string FieldValue { get; init; } = string.Empty;
    }

    public sealed class ProductsSpec : GridSpecificationBase<Product>
    {
        public ProductsSpec(GridQueryModel gridQueryModel)
        {
            AddInclude(x => x.Returns);
            AddInclude(x => x.Code);

            foreach (var filterModel in gridQueryModel.Filters)
            {
                Criterias.Add(PredicateBuilder.Build<Product>(filterModel.FieldName, filterModel.Comparision, filterModel.FieldValue));
            }

            foreach (var sort in gridQueryModel.Sorts)
            {
                ApplySorting(sort);
            }

            ApplyPaging(gridQueryModel.Page, gridQueryModel.PageSize);
        }
    }
}
