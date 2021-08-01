using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using FluentValidation;
using MediatR;
using N8T.Core.Domain;
using N8T.Core.Repository;
using ProductService.AppCore.Core;
using ProductService.AppCore.Core.Specs;

namespace ProductService.AppCore.UseCases.Queries
{
    public class GetProductById
    {
        public record Query : IItemQuery<Guid, ProductDto>
        {
            public List<string> Includes { get; init; } = new(new[] {"Returns", "Code"});
            public Guid Id { get; init; }

            internal class Validator : AbstractValidator<Query>
            {
                public Validator()
                {
                    RuleFor(x => x.Id)
                        .NotNull()
                        .NotEmpty().WithMessage("Id is required.");
                }
            }

            internal class Handler : IRequestHandler<Query, ResultModel<ProductDto>>
            {
                private readonly IRepository<Product> _productRepository;

                public Handler(IRepository<Product> productRepository)
                {
                    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                }

                public async Task<ResultModel<ProductDto>> Handle(Query request,
                    CancellationToken cancellationToken)
                {
                    if (request == null) throw new ArgumentNullException(nameof(request));

                    var spec = new ProductByIdQuerySpec<ProductDto>(request);

                    var product = await _productRepository.FindOneAsync(spec);

                    return ResultModel<ProductDto>.Create(new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Active = product.Active,
                        Cost = product.Cost,
                        Quantity = product.Quantity,
                        Created = product.Created,
                        Updated = product.Updated,
                        ProductCodeId = product.ProductCodeId
                    });
                }
            }
        }
    }
}
