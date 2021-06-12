using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductService.CoreApp
{
    public static class Extensions
    {
        public static IServiceCollection AddAppCoreModule(this IServiceCollection services,
            IConfiguration config, IWebHostEnvironment env)
        {
            services.AddCore()
        }
    }
}
