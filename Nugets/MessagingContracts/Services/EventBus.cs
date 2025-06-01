namespace Packages.Messaging.Masstransit.Services;

using MassTransit;
using Microsoft.AspNetCore.Http;

public class EventBus(
    IBus bus,
    IHttpContextAccessor httpContextAccessor)
    : IEventBus
{
    private const string CorrelationIdHeader = "X-Correlation-ID";

    public async Task PublishAsync<T>(T message)
        where T : class
    {
        var correlationId = ExtractCorrelationId(httpContextAccessor);
        await bus.Publish(message, context =>
        {
            if (!string.IsNullOrWhiteSpace(correlationId))
            {
                context.Headers.Set(CorrelationIdHeader, correlationId);

                if (Guid.TryParse(correlationId, out var guid))
                {
                    context.CorrelationId = guid;
                }
            }
        });
    }

    private static string? ExtractCorrelationId(IHttpContextAccessor httpContextAccessor)
    {
        var httpContext = httpContextAccessor.HttpContext;

        return httpContext?.Request?.Headers[CorrelationIdHeader].FirstOrDefault();
    }
}
