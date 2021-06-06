using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using FluentValidation;
using MediatR;
using N8T.Core.Domain;
using N8T.Core.Repository;
using ProductService.Core.Entities;
using ProductService.Core.Specs;

namespace ProductService.Application.V1.UseCases.Queries
{
    public class GetProducts
    {
        public class Query : IListQuery<ListResponseModel<ProductDto>>
        {
            public List<string> Includes { get; init; } = new(new[] {"Returns", "Code"});
            public List<FilterModel> Filters { get; init; } = new();
            public List<string> Sorts { get; init; } = new();
            public int Page { get; init; } = 1;
            public int PageSize { get; init; } = 20;

            internal class Validator : AbstractValidator<Query>
            {
                public Validator()
                {
                    RuleFor(x => x.Page)
                        .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

                    RuleFor(x => x.PageSize)
                        .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
                }
            }

            internal class Handler : IRequestHandler<Query, ResultModel<ListResponseModel<ProductDto>>>
            {
                private readonly IGridRepository<Product> _productRepository;

                public Handler(IGridRepository<Product> productRepository)
                {
                    _productRepository =
                        productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                }

                public async Task<ResultModel<ListResponseModel<ProductDto>>> Handle(Query request,
                    CancellationToken cancellationToken)
                {
                    if (request == null) throw new ArgumentNullException(nameof(request));

                    var spec = new ProductListQuerySpec<ProductDto>(request);

                    var products = await _productRepository.FindAsync(spec);

                    var productModels = products.Select(x => x.AdaptToDto());

                    var totalProducts = await _productRepository.CountAsync(spec);

                    var resultModel = ListResponseModel<ProductDto>.Create(
                        productModels.ToList(), totalProducts, request.Page, request.PageSize);

                    return ResultModel<ListResponseModel<ProductDto>>.Create(resultModel);
                }
            }
        }
    }
}
