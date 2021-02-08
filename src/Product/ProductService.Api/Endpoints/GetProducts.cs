using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Endpoint;
using ProductService.Application.Queries;

namespace ProductService.Api.Endpoints
{
    public class GetProducts : BaseAsyncEndpoint
    {
        [HttpPost("/api/products-query")]
        public async Task<ActionResult> HandleAsync([FromBody] GetProductsRequest queryModel,
            CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(queryModel, cancellationToken));
        }
    }
}
