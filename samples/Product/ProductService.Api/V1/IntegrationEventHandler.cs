using System;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.IntegrationEvents.Customer;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using N8T.Infrastructure.Controller;

namespace ProductService.Application.V1
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/[controller]")]
    public class IntegrationEventHandler : BaseController
    {
        private readonly ILogger<IntegrationEventHandler> _logger;

        public IntegrationEventHandler(ILogger<IntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("CustomerCreated")]
        [Topic("pubsub", "CustomerCreatedIntegrationEvent")]
#pragma warning disable 1998
        public async Task<ActionResult> HandleCustomerCreatedAsync(CustomerCreatedIntegrationEvent @event,
#pragma warning restore 1998
            CancellationToken cancellationToken = new())
        {
            _logger.LogInformation($"I received the message with name={@event.GetType().FullName}");

            // TODO: this is an example for pub/sub
            return Ok();
        }
    }
}
