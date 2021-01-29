using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using N8T.Core.Repository;
using N8T.Infrastructure.App.Dtos;
using N8T.Infrastructure.EfCore;
using ProductService.Core.Entities;

namespace ProductService.Application.Queries
{
    public class GetProductByIdRequest : IRequest<ProductDto>
    {
        public Guid ProductId { get; init; }

        internal class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
        {
            public GetProductByIdRequestValidator()
            {
            }
        }

        internal class GetProductByIdRequestHandler : IRequestHandler<GetProductByIdRequest, ProductDto>
        {
            private readonly IRepository<Product> _productRepository;

            public GetProductByIdRequestHandler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            }

            public Task<ProductDto> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
            {
                if (request == null) throw new ArgumentNullException(nameof(request));

                var product = _productRepository.FindById(request.ProductId);

                return Task.FromResult(new ProductDto());
            }
        }
    }
}
