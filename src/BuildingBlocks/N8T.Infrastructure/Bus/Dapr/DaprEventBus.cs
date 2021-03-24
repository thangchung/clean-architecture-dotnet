using System;
using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.Extensions.Logging;
using N8T.Core.Domain;

namespace N8T.Infrastructure.Bus.Dapr
{
    public class DaprEventBus : IEventBus
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<DaprEventBus> _logger;

        public DaprEventBus(DaprClient daprClient, ILogger<DaprEventBus> logger)
        {
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishAsync<TEvent>(TEvent @event, string[] topics = default,
            CancellationToken token = default) where TEvent : IntegrationEventBase
        {
            var attr = (DaprPubSubNameAttribute)Attribute.GetCustomAttribute(typeof(TEvent),
                typeof(DaprPubSubNameAttribute));

            var pubsubName = "pubsub";

            if (attr is not null)
            {
                pubsubName = attr.PubSubName;
            }

            if (topics is null)
            {
                var topicName = @event.GetType().Name;

                _logger.LogInformation("Publishing event {@Event} to {PubsubName}.{TopicName}", @event, pubsubName, topicName);
                await _daprClient.PublishEventAsync(pubsubName, topicName, @event, token);
            }
            else
            {
                foreach (var topicName in topics)
                {
                    _logger.LogInformation("Publishing event {@Event} to {PubsubName}.{TopicName}", @event, pubsubName,
                        topicName);
                    await _daprClient.PublishEventAsync(pubsubName, topicName, @event, token);
                }
            }
        }

        public Task SubscribeAsync<TMessage>(string[] topics = default, CancellationToken token = default) where TMessage : IntegrationEventBase
        {
            throw new System.NotImplementedException();
        }
    }
}
