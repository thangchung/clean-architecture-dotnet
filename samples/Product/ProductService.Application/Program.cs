using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N8T.Core.Repository;
using N8T.Infrastructure;
using N8T.Infrastructure.Bus;
using N8T.Infrastructure.EfCore;
using N8T.Infrastructure.ServiceInvocation.Dapr;
using N8T.Infrastructure.Swagger;
using N8T.Infrastructure.TransactionalOutbox;
using ProductService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

const string corsName = "api";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsName, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddCore(new[] {typeof(Anchor)})
    .AddPostgresDbContext<MainDbContext, ProductService.Infrastructure.Anchor>(
        builder.Configuration.GetConnectionString("postgres"),
        svc =>
        {
            svc.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            svc.AddScoped(typeof(IGridRepository<>), typeof(Repository<>));
        })
    .AddDaprClient()
    .AddControllers()
    .AddMessageBroker(builder.Configuration)
    .AddTransactionalOutbox(builder.Configuration)
    .AddSwagger<Anchor>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(corsName);
app.UseRouting();
app.UseCloudEvents();

app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
    endpoints.MapDefaultControllerRoute();
});

var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
app.UseSwagger(provider);

app.Run();

internal struct Anchor {}
