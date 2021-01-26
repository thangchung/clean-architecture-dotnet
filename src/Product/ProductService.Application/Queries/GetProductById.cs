using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using N8T.Infrastructure.App.Dtos;

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
            public Task<ProductDto> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
