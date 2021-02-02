using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using N8T.Core.Repository;
using N8T.Infrastructure.App.Dtos;
using ProductService.Core.Entities;
using ProductService.Core.Specifications;

namespace ProductService.Application.Queries
{
    public class GridResultModel<T>
    {
        public List<T> Items { get; init; } = new();
        public long TotalItems { get; init; } = 0;
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }

    public class GetProductsRequest: IRequest<GridResultModel<ProductDto>>
    {
        public GridQueryModel QueryModel { get; init; } = new();

        internal class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
        {
            public GetProductsRequestValidator()
            {
            }
        }

        internal class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, GridResultModel<ProductDto>>
        {
            private readonly IGridRepository<Product> _productRepository;

            public GetProductsRequestHandler(IGridRepository<Product> productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            }

            public async Task<GridResultModel<ProductDto>> Handle(GetProductsRequest request,
                CancellationToken cancellationToken)
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var spec = new ProductsSpec(request.QueryModel);

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
                }).ToList();

                var totalProducts = await _productRepository.CountAsync(spec);

                var resultModel = new GridResultModel<ProductDto>
                {
                    Page = request.QueryModel.Page,
                    PageSize = request.QueryModel.PageSize,
                    Items = productModels,
                    TotalItems = totalProducts
                };

                return resultModel;
            }
        }
    }
}
