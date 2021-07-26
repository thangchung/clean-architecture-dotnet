using CoolStore.AppContracts;
using CoolStore.AppContracts.RestApi;
using CustomerService.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N8T.Infrastructure;
using N8T.Infrastructure.Bus;
using N8T.Infrastructure.EfCore;
using N8T.Infrastructure.TransactionalOutbox;
using N8T.Infrastructure.ServiceInvocation.Dapr;
using AppCoreAnchor = CustomerService.AppCore.Anchor;

namespace CustomerService.Infrastructure
{
    public static class Extensions
    {
        private const string CorsName = "api";
        private const string DbName = "postgres";

        public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration config, IWebHostEnvironment env)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsName, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor();

            services.AddCustomMediatR(new[] {typeof(AppCoreAnchor)});
            services.AddCustomValidators(new[] {typeof(AppCoreAnchor)});

            services.AddDaprClient();
            services.AddMessageBroker(config);
            services.AddTransactionalOutbox(config);

            services.AddPostgresDbContext<MainDbContext>(
                config.GetConnectionString(DbName),
                dbOptionsBuilder => dbOptionsBuilder.UseModel(MainDbContextModel.Instance),
                svc => svc.AddRepository(typeof(Repository<>)));

            services.AddRestClient(typeof(ICountryApi), AppConstants.SettingAppName,
                config.GetValue("Services:SettingApp:Port", 5005));

            services.AddSwaggerGen();

            return services;
        }

        public static IApplicationBuilder UseCoreApplication(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsName);
            app.UseRouting();

            app.UseCloudEvents();
            app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

            app.UseSwagger();
            return app.UseSwaggerUI();
        }
    }
}
