using CustomerInventoryService.Application.CQRS.Commands.Handlers;
using CustomerInventoryService.Endpoints;
using CustomerInventoryService.Infrastructure.Configuration.ServiceBus;
using Mediatr.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.RegisterMediatr(typeof(AddProductCommandHandler).Assembly);

var app = builder.Build();

app.UseHttpsRedirection();

app.RegisterEndpoints();

await app.RunAsync();