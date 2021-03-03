using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using N8T.Core.Domain;
using N8T.Core.Repository;
using N8T.Infrastructure.Endpoint;
using ProductService.Core.Entities;
using ProductService.Core.Specifications;

namespace ProductService.Application.Endpoints.Queries
{
    public class GetProductById : BaseAsyncEndpoint.WithRequest<Guid>.WithResponse<ProductDto>
    {
        [HttpGet("/api/products/{id:guid}")]
        public override async Task<ActionResult<ProductDto>> HandleAsync(Guid id,
            CancellationToken cancellationToken = new())
        {
            var request = new Query {Id = id};

            return Ok(await Mediator.Send(request, cancellationToken));
        }

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

                    var spec = new ProductByIdQuerySpec(request);

                    var product = await _productRepository.FindOneAsync(spec);

                    return new ResultModel<ProductDto>
                    (
                        new ProductDto
                        {
                            Id = product.Id,
                            ProductCodeId = product.ProductCodeId,
                            Active = product.Active,
                            Cost = product.Cost,
                            Name = product.Name,
                            Quantity = product.Quantity,
                            ProductCodeName = product.Code.Name,
                            Created = product.Created,
                            Modified = product.Updated
                        }
                    );
                }
            }
        }
    }
}
