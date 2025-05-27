namespace NotificationService.Infrastructure.Configuration.ServiceBus;

using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotificationService.Application.Consummers;
using NotificationService.Application.Interfaces;
using NotificationService.Infrastructure.Configuration.ServiceBus.Options;

public static class Servicebus
{
    private const string ServiceBusKey = "ServiceBus";

    public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var servicebusOptions = services.ConfigureAndGetServiceBusOption(configuration);

        services.AddMassTransit<IServicebus>(x =>
        {
            x.RegisterConsummers();

            x.ConfigureEndpointNaming();

            x.UsingRabbitMq((_, cfg) =>
            {
                cfg.Host(servicebusOptions.ConnectionString);
            });
        });
    }

    private static void RegisterConsummers(this IBusRegistrationConfigurator configurator)
    {
        var consummers = new Assembly[] { typeof(SendReminderConsumer).Assembly };
        configurator.AddConsumers(consummers);
    }

    private static void ConfigureEndpointNaming(this IBusRegistrationConfigurator configurator)
    {
        configurator.SetKebabCaseEndpointNameFormatter();
    }

    private static ServiceBusConfigOptions ConfigureAndGetServiceBusOption(this IServiceCollection services, IConfiguration configuration)
    {
        services
         .Configure<ServiceBusConfigOptions>(configuration.GetSection(ServiceBusKey))
         .AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<ServiceBusConfigOptions>>().Value);

        using var provider = services.BuildServiceProvider();

        return provider.GetRequiredService<IOptions<ServiceBusConfigOptions>>().Value;
    }
}
