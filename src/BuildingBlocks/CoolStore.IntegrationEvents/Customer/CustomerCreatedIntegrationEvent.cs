using N8T.Core.Domain;
using N8T.Infrastructure.Bus.Dapr;

namespace CoolStore.IntegrationEvents.Customer
{
    [DaprPubSubName(PubSubName = "pubsub")]
    public class CustomerCreatedIntegrationEvent : EventBase
    {
        public override void Flatten()
        {
        }
    }
}
