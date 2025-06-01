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
using Packages.Mediatr.Configurations;
using Packages.Middlewares.Audit;
using Packages.Middlewares.Exceptions;
using Packages.Middlewares.Logging;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .ConfigureApplicationSettings(builder.Configuration)
    .AddOpenApi()
    .ConfigureApiVersioning()
    .ConfigureSwaggerUI()
    .ConfigureMassTransit(builder.Configuration)
    .RegisterMediatr(typeof(AddProductCommandHandler).Assembly)
    .RegisterValidation()
    .RegisterAuditLog()
    .AddHttpContextAccessor()
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