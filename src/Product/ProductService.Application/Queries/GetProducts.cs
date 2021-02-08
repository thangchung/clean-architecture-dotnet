using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using N8T.Core.Domain;
using N8T.Core.Repository;
using N8T.Infrastructure.App.Dtos;
using ProductService.Core.Entities;
using ProductService.Core.Specifications;

namespace ProductService.Application.Queries
{
    public record GetProductsRequest : IQuery<ResultModel<GetProductsRequest.ListResponseModel<ProductDto>>>, IListQueryModel
    {
        public List<string> Includes { get; init; } = new();
        public List<FilterModel> Filters { get; init; } = new();
        public List<string> Sorts { get; init; } = new();
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;

        internal record ListResponseModel<T>(List<T> Items, long TotalItems, int Page, int PageSize);

        internal class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
        {
            public GetProductsRequestValidator()
            {
            }
        }

        internal class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, ResultModel<ListResponseModel<ProductDto>>>
        {
            private readonly IGridRepository<Product> _productRepository;

            public GetProductsRequestHandler(IGridRepository<Product> productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            }

            public async Task<ResultModel<ListResponseModel<ProductDto>>> Handle(GetProductsRequest request,
                CancellationToken cancellationToken)
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var spec = new ProductListQuerySpec(request);

                var products = await _productRepository.FindAsync(spec);

                var productModels = products.Select(x => new ProductDto
                {
                    Id = x.Id,
                    ProductCodeId = x.ProductCodeId,
                    Active = x.Active,
                    Cost = x.Cost,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    ProductCodeName = x.Code.Name,
                    Created = x.Created,
                    Modified = x.Updated
                });

                var totalProducts = await _productRepository.CountAsync(spec);

                var resultModel =
                    new ListResponseModel<ProductDto>(productModels.ToList(), totalProducts, request.Page, request.PageSize);

                return new ResultModel<ListResponseModel<ProductDto>>(resultModel);
            }
        }
    }
}
