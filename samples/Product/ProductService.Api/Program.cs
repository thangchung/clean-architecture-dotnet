using Microsoft.AspNetCore.Builder;
using ProductService.Infrastructure;
using ApiAnchor = ProductService.Application.V1.Anchor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices(builder.Configuration, builder.Environment, typeof(ApiAnchor));

var app = builder.Build();
app.UseCoreApplication(builder.Environment);

app.Run();
