using CustomerInventoryService.Endpoints;
using CustomerInventoryService.Infrastructure.Configuration.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.ConfigureMassTransit(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.RegisterEndpoints();

await app.RunAsync();