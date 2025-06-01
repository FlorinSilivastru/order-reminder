namespace Packages.Messaging.Masstransit.Services;

using MassTransit;

public interface IEventBus
{
    Task PublishAsync<T>(T message)
        where T : class;
}
