using NotificationService.Infrastructure.Configuration.HealthCheck;
using NotificationService.Infrastructure.Configuration.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()
    .ConfigureMassTransit(builder.Configuration);

var app = builder.Build();

app.MapHealthCheckEndpoints();

await app.RunAsync();
