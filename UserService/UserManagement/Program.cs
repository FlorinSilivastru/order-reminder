using Middlewares.Audit;
using Middlewares.Exceptions;
using Middlewares.Logging;
using UserManagement.Endpoints;
using UserManagement.Infrastructure.Configuration.ApiVersioning;
using UserManagement.Infrastructure.Configuration.Middleware;
using UserManagement.Infrastructure.Configuration.SwaggerUI;
using UserManagement.Infrastructure.Configuration.Validation;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddOpenApi()
    .ConfigureApiVersioning()
    .ConfigureSwaggerUI()
    .RegisterValidation()
    .RegisterAuditLog()
    .AddHttpContextAccessor();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<CorrelationIdMiddleware>();

app.UseMiddleware<AuditLogMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

app.RegisterEndpoints();

await app.RunAsync();