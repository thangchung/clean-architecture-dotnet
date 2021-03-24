using System;

namespace N8T.Infrastructure.Bus.Dapr
{
    public class DaprPubSubNameAttribute : Attribute
    {
        public string PubSubName { get; set; } = "pubsub";
    }
}
