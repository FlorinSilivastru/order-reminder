using CustomerInventoryService.Application.CQRS.Commands.Handlers;
using CustomerInventoryService.Infrastructure.Configuration.ApiVersioning;
using CustomerInventoryService.Infrastructure.Configuration.Authenfication;
using CustomerInventoryService.Infrastructure.Configuration.Authorization;
using CustomerInventoryService.Infrastructure.Configuration.Middleware;
using CustomerInventoryService.Infrastructure.Configuration.ServiceBus;
using CustomerInventoryService.Infrastructure.Configuration.Settings;
using CustomerInventoryService.Infrastructure.Configuration.SwaggerUI;
using CustomerInventoryService.Infrastructure.Configuration.Validation;
using CustomerInventoryServiceApi.Endpoints;
using Mediatr.Configurations;
using Middlewares.Audit;
using Middlewares.Exceptions;
using Middlewares.Logging;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddOpenApi()
    .ConfigureApiVersioning()
    .ConfigureSwaggerUI()
    .ConfigureMassTransit(builder.Configuration)
    .RegisterMediatr(typeof(AddProductCommandHandler).Assembly)
    .RegisterValidation()
    .RegisterAuditLog()
    .AddHttpContextAccessor()
    .ConfigureApplicationSettings(builder.Configuration)
    .ConfigureAuthentification()
    .ConfigureAuthorization()
    .AddHealthChecks();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseMiddleware<AuditLogMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

app.RegisterEndpoints();

await app.RunAsync();