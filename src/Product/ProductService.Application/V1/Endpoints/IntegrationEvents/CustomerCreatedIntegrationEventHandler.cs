using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.IntegrationEvents.Customer;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using N8T.Infrastructure.Endpoint;

namespace ProductService.Application.V1.Endpoints.IntegrationEvents
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/[controller]")]
    public class CustomerCreatedIntegrationEventHandler : BaseAsyncEndpoint.WithRequest<CustomerCreatedIntegrationEvent>.WithoutResponse
    {
        private readonly ILogger<CustomerCreatedIntegrationEventHandler> _logger;

        public CustomerCreatedIntegrationEventHandler(ILogger<CustomerCreatedIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("CustomerCreated")]
        [Topic("pubsub", "CustomerCreatedIntegrationEvent")]
        public override async Task<ActionResult> HandleAsync(CustomerCreatedIntegrationEvent @event,
            CancellationToken cancellationToken = new ())
        {
            _logger.LogInformation($"I received the message with name={@event.GetType().FullName}");

            // TODO: this is an example for pub/sub
            return Ok();
        }
    }
}
