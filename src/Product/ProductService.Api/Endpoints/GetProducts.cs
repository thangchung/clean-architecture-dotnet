using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Endpoint;

namespace ProductService.Api.Endpoints
{
    public class GetProducts : BaseAsyncEndpoint
    {
        [HttpPost("/api/products-query")]
        public async Task<ActionResult> HandleAsync([FromBody] Application.Queries.GetProducts.Query queryModel,
            CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(queryModel, cancellationToken));
        }
    }
}
