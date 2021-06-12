using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using N8T.Infrastructure.Controller;
using N8T.Infrastructure.TransactionalOutbox.Dapr;

namespace ProductService.Application.V1
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TransactionalOutboxProcessor : BaseController
    {
        private readonly ITransactionalOutboxProcessor _outboxProcessor;

        public TransactionalOutboxProcessor(ITransactionalOutboxProcessor outboxProcessor)
        {
            _outboxProcessor = outboxProcessor ?? throw new ArgumentNullException(nameof(outboxProcessor));
        }

        [HttpPost("ProductOutboxCron")]
        public async Task<ActionResult> HandleProductOutboxCronAsync(CancellationToken cancellationToken = new())
        {
            await _outboxProcessor.HandleAsync(typeof(CoolStore.IntegrationEvents.Anchor), cancellationToken);

            return Ok();
        }
    }
}
