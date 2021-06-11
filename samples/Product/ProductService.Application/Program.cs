using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure;
using N8T.Infrastructure.EfCore;
using ProductService.Application.V1;
using ProductService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore(builder.Configuration, typeof(Anchor))
    .AddPostgresDbContext<MainDbContext, ProductService.Infrastructure.Anchor>(
        builder.Configuration.GetConnectionString("postgres"),
        svc => svc.AddRepository(typeof(Repository<>)));

var app = builder.Build();

app.UseAppCore(builder.Environment);

app.Run();
