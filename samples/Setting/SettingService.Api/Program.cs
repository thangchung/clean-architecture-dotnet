using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using SettingService.AppCore.UseCases.Queries;
using SettingService.Infrastructure;
using static N8T.Infrastructure.Result.ResultMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices(builder.Configuration, builder.Environment);

await using var app = builder.Build();
app.UseCoreApplication(builder.Environment);

app.MapGet("/api/v1/countries/{id}",
    async (Guid id, ISender sender) =>
        Ok(await sender.Send(new GetCountryById.Query {Id = id})));

await app.RunAsync();
