using System;
using N8T.Core.Domain;

namespace CoolStore.IntegrationEvents.Product
{
    public class ProductCodeCreatedIntegrationEvent : EventBase
    {
        public Guid ProductCodeId { get; set; }
        public string ProductCodeName { get; set; } = default!;

        public override void Flatten()
        {
            MetaData.Add("ProductCodeId", ProductCodeId);
            MetaData.Add("ProductCodeName", ProductCodeName);
        }
    }
}
