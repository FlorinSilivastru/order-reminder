using Middlewares.Audit;
using Middlewares.Exceptions;
using Middlewares.Logging;
using UserManagement.Infrastructure.Configuration.ApiVersioning;
using UserManagement.Infrastructure.Configuration.Authenfication;
using UserManagement.Infrastructure.Configuration.Authorization;
using UserManagement.Infrastructure.Configuration.Middleware;
using UserManagement.Infrastructure.Configuration.Settings;
using UserManagement.Infrastructure.Configuration.SwaggerUI;
using UserManagement.Infrastructure.Configuration.Validation;
using UserServiceApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddOpenApi()
    .ConfigureApiVersioning()
    .ConfigureSwaggerUI()
    .RegisterValidation()
    .RegisterAuditLog()
    .AddHttpContextAccessor()
    .ConfigureAuthentification()
    .ConfigureAuthorization()
    .ConfigureApplicationSettings(builder.Configuration)
    .AddHealthChecks();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseMiddleware<AuditLogMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

app.RegisterEndpoints();

await app.RunAsync();