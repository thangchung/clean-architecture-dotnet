var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices(builder.Configuration, builder.Environment);

await using WebApplication app = builder.Build();

app.UseCoreApplication(builder.Environment);

app.MapGet("/api/v1/products", async (HttpContext http) =>
{
    if (!http.Request.Headers.TryGetValue("x-query", out var query)) return BadRequest();

    var sender = http.RequestServices.GetService<ISender>();
    var queryModel = http.SafeGetListQuery<GetProducts.Query, ListResultModel<ProductDto>>(query);
    var result = await sender!.Send(queryModel);
    return Ok(result);
}).RequireAuthorization("ApiCaller");

app.MapGet("/api/v1/products/{id:guid}", async (Guid id, ISender sender) =>
{
    var request = new GetProductById.Query { Id = id };
    var result = await sender.Send(request);
    return Ok(result);
}).RequireAuthorization("ApiCaller");

app.MapPost("/api/v1/products",
    async (CreateProduct.Command request, ISender sender) =>
    {
        var result = await sender.Send(request);
        return Ok(result);
    }).RequireAuthorization("ApiCaller");

app.MapPost("/CustomerCreated",
        (CustomerCreatedIntegrationEvent @event) =>
        {
            Console.WriteLine($"I received the message with name={@event.GetType().FullName}");
            return Ok("Subscribed");
        })
    .WithTopic("pubsub", "CustomerCreatedIntegrationEvent");

app.MapPost("/ProductOutboxCron",
    async (ITxOutboxProcessor outboxProcessor) =>
        await outboxProcessor.HandleAsync(typeof(CoolStore.IntegrationEvents.Anchor)));

await app.RunAsync();
