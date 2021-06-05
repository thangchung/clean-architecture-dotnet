using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure.Bus.Dapr;
using N8T.Infrastructure.Bus.Dapr.Internal;

namespace N8T.Infrastructure.Bus
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBroker(this IMvcBuilder mvcBuilder,
            IConfiguration config,
            string messageBrokerType = "dapr")
        {
            switch (messageBrokerType)
            {
                case "dapr":
                    mvcBuilder.Services.Configure<DaprEventBusOptions>(config.GetSection(DaprEventBusOptions.Name));
                    mvcBuilder.AddDapr();
                    mvcBuilder.Services.AddScoped<IEventBus, DaprEventBus>();
                    break;
            }

            return mvcBuilder.Services;
        }
    }
}
