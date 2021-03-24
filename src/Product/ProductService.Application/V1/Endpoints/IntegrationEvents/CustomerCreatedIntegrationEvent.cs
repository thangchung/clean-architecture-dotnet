using System.Threading.Tasks;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Application.V1.Endpoints.IntegrationEvents
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerCreatedIntegrationEvent : ControllerBase
    {
        [HttpPost("CustomerCreated")]
        [Topic("pubsub", "CustomerCreatedIntegrationEvent")]
        public async Task HandleAsync(CoolStore.AppContracts.IntegrationEvents.CustomerCreatedIntegrationEvent @event)
        {
            // TODO: this is an example for pub/sub
        }
    }
}
