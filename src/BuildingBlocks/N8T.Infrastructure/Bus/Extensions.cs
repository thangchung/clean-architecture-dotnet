using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure.Bus.Dapr;

namespace N8T.Infrastructure.Bus
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBroker(this IMvcBuilder mvcBuilder,
            string messageBrokerType = "dapr")
        {
            switch (messageBrokerType)
            {
                case "dapr":
                    mvcBuilder.AddDapr();
                    mvcBuilder.Services.AddScoped<IEventBus, DaprEventBus>();
                    break;
            }

            return mvcBuilder.Services;
        }
    }
}
