using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using N8T.Infrastructure;
using N8T.Infrastructure.Helpers;

namespace ProductService.Api
{
    /*public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var (hostBuilder, isRunOnTye) = HostHelper.CreateHostBuilder<Startup>(args);
            return await hostBuilder.RunAsync(isRunOnTye);
        }
    }*/

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
