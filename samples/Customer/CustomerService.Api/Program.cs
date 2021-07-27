using CustomerService.AppCore.UseCases.Commands;
using Microsoft.AspNetCore.Builder;
using CustomerService.Infrastructure;
using MediatR;
using N8T.Infrastructure.TxOutbox;
using static N8T.Infrastructure.Result.ResultMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices(builder.Configuration, builder.Environment);

await using var app = builder.Build();
app.UseCoreApplication(builder.Environment);

app.MapPost("/api/v1/customers",
    async (CreateCustomer.Command request, ISender sender) => Ok(await sender.Send(request)));

app.MapPost("/CustomerOutboxCron",
    async (ITxOutboxProcessor outboxProcessor) =>
        await outboxProcessor.HandleAsync(typeof(CoolStore.IntegrationEvents.Anchor)));

await app.RunAsync();


