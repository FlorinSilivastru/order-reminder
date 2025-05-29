using CustomerInventoryService.Application.CQRS.Commands.Handlers;
using CustomerInventoryService.Endpoints;
using CustomerInventoryService.Infrastructure.Configuration.Middleware;
using CustomerInventoryService.Infrastructure.Configuration.ServiceBus;
using CustomerInventoryService.Infrastructure.Configuration.Validation;
using Mediatr.Configurations;
using Middlewares.Audit;
using Middlewares.Exceptions;
using Middlewares.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.RegisterMediatr(typeof(AddProductCommandHandler).Assembly);
builder.Services.RegisterValidation();
builder.Services.RegisterAuditLog();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseMiddleware<AuditLogMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

app.RegisterEndpoints();

await app.RunAsync();