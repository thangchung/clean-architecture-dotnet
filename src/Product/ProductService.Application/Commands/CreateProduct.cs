using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using N8T.Core.Domain;
using N8T.Core.Repository;
using N8T.Infrastructure.App.Dtos;
using N8T.Infrastructure.EfCore;
using ProductService.Core.Entities;

namespace ProductService.Application.Commands
{
    public class CreateProduct
    {
        public record Command : ICreateInput<Command.CreateProductModel>,
            ICommand<ProductDto>, ITxRequest
        {
            public CreateProductModel Model { get; init; }

            public record CreateProductModel(string Name, int Quantity, decimal Cost, string ProductCodeName);

            internal class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
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
                    _productRepository =
                        productRepository ?? throw new ArgumentNullException(nameof(productRepository));
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
