using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Endpoint;
using ProductService.Application.Queries;
using ProductService.Core.Specifications;

namespace ProductService.Api.Endpoints
{
    public class GetProducts : BaseAsyncEndpoint
    {
        [HttpPost("/api/products")]
        public async Task<ActionResult> HandleAsync([FromBody] GridQueryModel queryModel,
            CancellationToken cancellationToken = new())
            => Ok(await Mediator.Send(new GetProductsRequest {QueryModel = queryModel},
                cancellationToken));
    }
}
