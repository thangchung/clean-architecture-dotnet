using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N8T.Infrastructure;
using N8T.Infrastructure.EfCore;
using N8T.Infrastructure.Swagger;
using N8T.Infrastructure.Validator;
using SettingService.Infrastructure.Data;
using AppCoreAnchor = SettingService.AppCore.Anchor;

namespace SettingService.Infrastructure
{
    public static class Extensions
    {
        private const string CorsName = "api";
        private const string DbName = "postgres";

        public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration config, IWebHostEnvironment env, Type apiType)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsName, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddHttpContextAccessor();
            services.AddCustomMediatR(new[] {typeof(AppCoreAnchor)});
            services.AddCustomValidators(new[] {typeof(AppCoreAnchor)});
            services.AddControllers();
            services.AddSwagger(apiType);

            services.AddPostgresDbContext<MainDbContext>(
                config.GetConnectionString(DbName),
                dbOptionsBuilder => dbOptionsBuilder.UseModel(MainDbContextModel.Instance),
                svc => svc.AddRepository(typeof(Repository<>)));

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            return app.UseSwagger(provider);
        }
    }
}
