using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Endpoint;
using ProductService.Application.Queries;

namespace ProductService.Api.Endpoints
{
    public class GetProducts : BaseAsyncEndpoint
    {
        [HttpGet("/api/products")]
        public async Task<ActionResult> HandleAsync([FromQuery] int quantity, int page, int pageSize,
            CancellationToken cancellationToken = new())
            => Ok(await Mediator.Send(new GetProductsRequest {Quantity = quantity, Page = page, PageSize = pageSize},
                cancellationToken));
    }
}
