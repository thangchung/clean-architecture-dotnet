using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace N8T.Infrastructure.Swagger
{
    public static class Extentions
    {
        public static IServiceCollection AddSwagger<TMarker>(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();

                    var xmlFile = XmlCommentsFilePath();
                    if (File.Exists(xmlFile))
                    {
                        options.IncludeXmlComments(xmlFile);
                    }
                });

            return services;

            static string XmlCommentsFilePath()
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(TMarker).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });

            return app;
        }
    }
}
