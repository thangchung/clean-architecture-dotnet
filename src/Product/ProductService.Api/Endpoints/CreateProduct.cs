using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Endpoint;

namespace ProductService.Api.Endpoints
{
    public class CreateProduct : BaseAsyncEndpoint
    {
        [HttpPost("/api/products")]
        public async Task<ActionResult> HandleAsync([FromBody] Application.Commands.CreateProduct.Command model,
            CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(model, cancellationToken));
        }
    }
}
