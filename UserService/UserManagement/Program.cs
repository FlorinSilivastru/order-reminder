using Packages.Middlewares.Audit;
using Packages.Middlewares.Exceptions;
using Packages.Middlewares.Logging;
using UserService.Infrastructure.Configuration.ApiVersioning;
using UserService.Infrastructure.Configuration.Authenfication;
using UserService.Infrastructure.Configuration.Authorization;
using UserService.Infrastructure.Configuration.Middleware;
using UserService.Infrastructure.Configuration.Settings;
using UserService.Infrastructure.Configuration.SwaggerUI;
using UserService.Infrastructure.Configuration.Validation;
using UserServiceApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .ConfigureApplicationSettings(builder.Configuration)
    .AddOpenApi()
    .ConfigureApiVersioning()
    .ConfigureSwaggerUI()
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