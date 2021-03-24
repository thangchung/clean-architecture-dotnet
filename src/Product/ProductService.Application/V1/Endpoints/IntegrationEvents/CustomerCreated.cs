using System.Threading;
using System.Threading.Tasks;
using CoolStore.AppContracts.IntegrationEvents;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Endpoint;

namespace ProductService.Application.V1.Endpoints.IntegrationEvents
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/[controller]")]
    public class CustomerCreated : BaseAsyncEndpoint.WithRequest<CustomerCreatedIntegrationEvent>.WithoutResponse
    {
        // [HttpPost("CustomerCreated")]
        // [Topic("pubsub", "CustomerCreatedIntegrationEvent")]
        // public override async Task HandleAsync(CustomerCreatedIntegrationEvent @event)
        // {
        //     // TODO: this is an example for pub/sub
        // }

        [HttpPost("CustomerCreated")]
        [Topic("pubsub", "CustomerCreatedIntegrationEvent")]
        public override async Task<ActionResult> HandleAsync(CustomerCreatedIntegrationEvent @event,
            CancellationToken cancellationToken = new ())
        {
            // TODO: this is an example for pub/sub
            return Ok();
        }
    }
}
