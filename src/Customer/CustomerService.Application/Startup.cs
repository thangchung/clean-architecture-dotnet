using CoolStore.AppContracts;
using CoolStore.AppContracts.RestApi;
using CustomerService.Infrastructure.Data;
using Dapr.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N8T.Core.Repository;
using N8T.Infrastructure;
using N8T.Infrastructure.Dapr;
using N8T.Infrastructure.EfCore;
using N8T.Infrastructure.Swagger;
using N8T.Infrastructure.Tye;
using RestEase;
using RestEase.HttpClientFactory;

namespace CustomerService.Application
{
    public class Startup
    {
        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            Config = config;
            Env = env;
        }

        private IConfiguration Config { get; }
        private IWebHostEnvironment Env { get; }
        private bool IsRunOnTye => Config.IsRunOnTye();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("api", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddCore(new[] {typeof(Startup)})
                .AddPostgresDbContext<MainDbContext, Infrastructure.Anchor>(
                    Config.GetConnectionString("postgres"),
                    svc =>
                    {
                        svc.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                        svc.AddScoped(typeof(IGridRepository<>), typeof(Repository<>));
                    })
                .AddCustomDaprClient()
                .AddControllers()
                .AddDapr()
                .Services.AddSwagger<Startup>();

            var settingAppUri = IsRunOnTye
                ? $"http://{AppConsts.SettingAppName}:5005"
                : "http://localhost:5005"; //TODO: it might have a problem when deploy on k8s 

            services.AddScoped<InvocationHandler>();
            services.AddRestEaseClient<ICountryApi>(settingAppUri, client =>
            {
                client.RequestPathParamSerializer = new StringEnumRequestPathParamSerializer();
            }).AddHttpMessageHandler<InvocationHandler>();
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("api");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger(provider);
        }
    }
}
