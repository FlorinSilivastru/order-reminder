using NotificationService.Infrastructure.Configuration.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureMassTransit(builder.Configuration);

var app = builder.Build();

await app.RunAsync();
