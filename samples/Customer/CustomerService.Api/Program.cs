using CoolStore.AppContracts;
using CoolStore.AppContracts.RestApi;
using CustomerService.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure;
using N8T.Infrastructure.EfCore;
using N8T.Infrastructure.ServiceInvocation.Dapr;
using CustomerService.Application.V1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore(builder.Configuration, typeof(Anchor))
    .AddPostgresDbContext<MainDbContext>(
        builder.Configuration.GetConnectionString("postgres"),
        svc => svc.AddRepository(typeof(Repository<>)))
    .AddRestClient(typeof(ICountryApi), AppConsts.SettingAppName,
        builder.Configuration.GetValue("Services:SettingApp:Port", 5005));

var app = builder.Build();

app.UseAppCore(builder.Environment);

app.Run();
