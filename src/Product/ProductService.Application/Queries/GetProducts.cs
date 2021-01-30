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
    public class GetProductsRequest: IRequest<List<ProductDto>>
    {
        public int Quantity { get; init; } = 1000;
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;

        internal class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
        {
            public GetProductsRequestValidator()
            {
            }
        }

        internal class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, List<ProductDto>>
        {
            private readonly IRepository<Product> _productRepository;

            public GetProductsRequestHandler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            }

            public async Task<List<ProductDto>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var spec = new ProductsSpec("Quantity", ">", $"{request.Quantity}", request.Page, request.PageSize);

                var products = await _productRepository.FindAsync(spec);

                return products.Select(x => new ProductDto
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
            }
        }
    }
}
