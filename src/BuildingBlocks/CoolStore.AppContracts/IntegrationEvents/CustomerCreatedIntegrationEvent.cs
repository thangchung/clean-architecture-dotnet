using N8T.Core.Domain;
using N8T.Infrastructure.Bus.Dapr;

namespace CoolStore.AppContracts.IntegrationEvents
{
    [DaprPubSubName(PubSubName = "pubsub")]
    public class CustomerCreatedIntegrationEvent : IntegrationEventBase
    {
        public override void Flatten()
        {
        }
    }
}
