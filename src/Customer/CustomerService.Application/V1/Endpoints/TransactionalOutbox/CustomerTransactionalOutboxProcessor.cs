using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Endpoint;
using N8T.Infrastructure.TransactionalOutbox.Dapr;

namespace CustomerService.Application.V1.Endpoints.TransactionalOutbox
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CustomerTransactionalOutboxProcessor : BaseAsyncEndpoint.WithoutRequest.WithoutResponse
    {
        private readonly ITransactionalOutboxProcessor _outboxProcessor;

        public CustomerTransactionalOutboxProcessor(ITransactionalOutboxProcessor outboxProcessor)
        {
            _outboxProcessor = outboxProcessor ?? throw new ArgumentNullException(nameof(outboxProcessor));
        }

        [HttpPost("CustomerOutboxCron")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = new ())
        {
            await _outboxProcessor.HandleAsync(typeof(CoolStore.IntegrationEvents.Anchor), cancellationToken);

            return Ok();
        }
    }
}
