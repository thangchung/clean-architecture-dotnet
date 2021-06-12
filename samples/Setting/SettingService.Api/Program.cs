using Microsoft.AspNetCore.Builder;
using SettingService.Infrastructure;
using ApiAnchor = SettingService.Application.V1.Anchor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices(builder.Configuration, builder.Environment, typeof(ApiAnchor));

var app = builder.Build();
app.UseCoreApplication(builder.Environment);

app.Run();
