using MassTransit;

namespace Packages.Messaging.Masstransit.Services;

public interface IEventBus
{
    Task PublishAsync<T>(T message)
        where T : class;
}
