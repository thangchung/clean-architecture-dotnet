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

            /*_logger.LogInformation("{CustomerOutboxProcessor}: Cron @{DateTime}", nameof(CustomerTransactionalOutboxProcessor), DateTime.UtcNow);

            var events = await _daprClient.GetStateEntryAsync<List<OutboxEntity>>("statestore", "outbox");

            if (events?.Value != null && events?.Value.Count > 0)
            {
                var deletedEventIds = new List<Guid>();

                foreach (var domainEvent in events.Value)
                {
                    if (domainEvent.Id.Equals(Guid.Empty) || string.IsNullOrEmpty(domainEvent.Type) || string.IsNullOrEmpty(domainEvent.Data)) continue;

                    var @event = domainEvent.RecreateMessage(typeof(CoolStore.IntegrationEvents.Anchor).Assembly);

                    await _eventBus.PublishAsync(@event);

                    deletedEventIds.Add(domainEvent.Id);
                }

                if (deletedEventIds.Count > 0)
                {
                    foreach (var deletedEventId in deletedEventIds)
                    {
                        events.Value.RemoveAll(e => e.Id == deletedEventId);
                    }

                    await events.SaveAsync();
                }
            }*/

            return Ok();
        }
    }
}
