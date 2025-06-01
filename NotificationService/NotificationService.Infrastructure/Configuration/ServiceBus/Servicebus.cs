namespace NotificationService.Infrastructure.Configuration.ServiceBus;

using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using NotificationService.Application.Consummers;
using NotificationService.Infrastructure.Configuration.ServiceBus.Options;
using Packages.Messaging.Masstransit.Services;
using System.Reflection;

public static class Servicebus
{
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var servicebusOptions = services.ConfigureAndGetServiceBusOption(configuration);

        services.AddMassTransit<IBus>(x =>
        {
            x.ConfigureHealthChecks();

            x.RegisterConsummers();

            x.ConfigureEndpointNaming();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(servicebusOptions.ConnectionString);

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddTransient<IEventBus, EventBus>();

        return services;
    }

    private static void ConfigureHealthChecks(this IBusRegistrationConfigurator x)
    {
        x.ConfigureHealthCheckOptions(options =>
        {
            options.Name = "masstransit";
            options.MinimalFailureStatus = HealthStatus.Unhealthy;
            options.Tags.Add("health");
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
         .Configure<ServiceBusConfigOptions>(configuration.GetSection(nameof(ServiceBusConfigOptions)))
         .AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<ServiceBusConfigOptions>>().Value);

        using var provider = services.BuildServiceProvider();

        return provider.GetRequiredService<IOptions<ServiceBusConfigOptions>>().Value;
    }
}
