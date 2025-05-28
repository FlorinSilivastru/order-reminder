namespace Mediatr;

using global::Mediatr.Contracts.Common;
using global::Mediatr.Contracts.Handlers;
using global::Mediatr.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

public class Mediatr(IServiceProvider serviceProvider) : IMediatr
{
    public async Task SendAsync<T>(T command)
        where T : IRequest
    {
        ArgumentNullException.ThrowIfNull(command);

        await using var scope = serviceProvider.CreateAsyncScope();
        await scope.ServiceProvider
            .GetRequiredService<IRequestHandler<T>>()
            .HandleAsync(command);
    }

    public async Task<TResult> SendAsync<T, TResult>(T query)
      where T : IRequest<TResult>
      where TResult : class
    {
        ArgumentNullException.ThrowIfNull(query);

        await using var scope = serviceProvider.CreateAsyncScope();

        var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<T, TResult>>();

        return await handler.HandleAsync(query);
    }
}
