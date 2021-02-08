using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.App.Dtos;
using N8T.Infrastructure.Endpoint;
using ProductService.Application.Queries;

namespace ProductService.Api.Endpoints
{
    public class GetProductById : BaseAsyncEndpoint<Guid, ProductDto>
    {
        [HttpGet("/api/products/{id:guid}")]
        public override async Task<ActionResult<ProductDto>> HandleAsync(Guid id,
            CancellationToken cancellationToken = new())
        {
            var request = new GetProductByIdRequest {Id = id};

            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}
