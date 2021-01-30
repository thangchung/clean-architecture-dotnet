using System;
using System.Linq.Expressions;
using N8T.Core.Specification;
using N8T.Infrastructure.LambdaExpression;
using ProductService.Core.Entities;

namespace ProductService.Core.Specifications
{
    public sealed class ProductsSpec : SpecificationBase<Product>
    {
        private readonly string _filterFieldName;
        private readonly string _filterComparision;
        private readonly string _filterValue;

        public ProductsSpec(string filterFieldName, string filterComparision, string filterValue, int page,
            int pageSize)
        {
            _filterFieldName = filterFieldName;
            _filterComparision = filterComparision;
            _filterValue = filterValue;

            AddInclude(x => x.Returns);
            AddInclude(x => x.Code);

            ApplySorting("nameDesc");
            ApplyPaging(page, pageSize);

            Criterias.Add(PredicateBuilder.Build<Product>(_filterFieldName, _filterComparision, $"{_filterValue}"));
            Criterias.Add(PredicateBuilder.Build<Product>("Name", "Contains", $"a"));
        }

        public override Expression<Func<Product, bool>> Criteria =>
            PredicateBuilder.Build<Product>(_filterFieldName, _filterComparision, $"{_filterValue}");
    }
}
