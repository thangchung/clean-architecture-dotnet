using System;
using System.Collections.Generic;
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

                    return ResultModel<ProductDto>.Create(product.AdaptToDto());
                }
            }
        }
    }
}
