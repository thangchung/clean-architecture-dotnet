using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.AppContracts.Dtos;
using FluentValidation;
using MediatR;
using N8T.Core.Domain;
using N8T.Core.Repository;
using ProductService.Core.Entities;

namespace ProductService.Application.V1.UseCases.Commands
{
    public class CreateProduct
    {
        public record Command : ICreateCommand<Command.CreateProductModel, ProductDto>
        {
            public CreateProductModel Model { get; init; } = default!;

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

                public Handler(IRepository<Product> productRepository,
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

                    return ResultModel<ProductDto>.Create(created.AdaptToDto());
                }
            }
        }
    }
}
