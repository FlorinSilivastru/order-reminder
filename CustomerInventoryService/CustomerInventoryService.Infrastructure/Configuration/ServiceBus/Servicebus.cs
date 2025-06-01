namespace CustomerInventoryService.Infrastructure.Configuration.ServiceBus;

using CustomerInventoryService.Infrastructure.Configuration.ServiceBus.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Packages.Messaging.Masstransit.Services;

public static class Servicebus
{
    private const string ServiceBusKey = "ServiceBus";

    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var servicebusOptions = services.ConfigureAndGetServiceBusOption(configuration);

        services.AddMassTransit<IBus>(x =>
        {
            x.ConfigureEndpointNaming();

            x.UsingRabbitMq((_, cfg) =>
            {
                cfg.Host(servicebusOptions.ConnectionString);
            });
        });

        services.AddTransient<IEventBus, EventBus>();

        return services;
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
