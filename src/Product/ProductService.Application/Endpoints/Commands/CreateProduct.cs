using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using N8T.Core.Domain;
using N8T.Core.Repository;
using N8T.Infrastructure.App.Dtos;
using N8T.Infrastructure.Endpoint;
using ProductService.Core.Entities;

namespace ProductService.Application.Endpoints.Commands
{
    public class CreateProduct : BaseAsyncEndpoint
    {
        [HttpPost("/api/products")]
        public async Task<ActionResult> HandleAsync([FromBody] Command model,
            CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(model, cancellationToken));
        }

        public record Command : ICreateCommand<Command.CreateProductModel, ProductDto>
        {
            public CreateProductModel Model { get; init; }

            public record CreateProductModel(string Name, int Quantity, decimal Cost, string ProductCodeName);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(v => v.Model.Name)
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

                    RuleFor(v => v.Model.ProductCodeName)
                        .NotEmpty().WithMessage("ProductCodeName is required.")
                        .MaximumLength(5).WithMessage("ProductCodeName must not exceed 5 characters.");

                    RuleFor(x => x.Model.Quantity)
                        .GreaterThanOrEqualTo(1).WithMessage("Quantity should at least greater than or equal to 1.");

                    RuleFor(x => x.Model.Cost)
                        .GreaterThanOrEqualTo(1000).WithMessage("Cost should be greater than 1000.");
                }
            }

            internal class Handler : IRequestHandler<Command, ResultModel<ProductDto>>
            {
                private readonly IRepository<Product> _productRepository;
                private readonly IRepository<ProductCode> _productCodeRepository;

                public Handler(
                    IRepository<Product> productRepository,
                    IRepository<ProductCode> productCodeRepository)
                {
                    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
                    _productCodeRepository = productCodeRepository ??
                                             throw new ArgumentNullException(nameof(productCodeRepository));
                }

                public async Task<ResultModel<ProductDto>> Handle(Command request,
                    CancellationToken cancellationToken)
                {
                    var productCode =
                        await _productCodeRepository.AddAsync(ProductCode.Create(request.Model.ProductCodeName));
                    if (productCode is null)
                    {
                        throw new Exception($"Couldn't find Product Code with name={request.Model.ProductCodeName}");
                    }

                    var created = await _productRepository.AddAsync(
                        Product.Create(
                            request.Model.Name,
                            request.Model.Quantity,
                            request.Model.Cost,
                            productCode));

                    return new ResultModel<ProductDto>(new ProductDto
                    {
                        Id = created.Id,
                        ProductCodeId = created.ProductCodeId,
                        Active = created.Active,
                        Cost = created.Cost,
                        Name = created.Name,
                        Quantity = created.Quantity,
                        ProductCodeName = productCode.Name,
                        Created = created.Created,
                        Modified = created.Updated
                    });
                }
            }
        }
    }
}
