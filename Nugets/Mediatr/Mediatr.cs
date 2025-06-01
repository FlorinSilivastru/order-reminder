namespace Packages.Mediatr;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Packages.Mediatr.Contracts.Common;
using Packages.Mediatr.Contracts.Handlers;
using Packages.Mediatr.Contracts.Services;

public class Mediatr(IServiceProvider serviceProvider) : IMediatr
{
    public async Task SendAsync<T>(T command)
        where T : IRequest
    {
        ArgumentNullException.ThrowIfNull(command);

        await using var scope = serviceProvider.CreateAsyncScope();

        await ValidatePayload(command, scope);

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

        await ValidatePayload(query, scope);

        var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<T, TResult>>();

        return await handler.HandleAsync(query);
    }

    private static async Task ValidatePayload<T>(T command, AsyncServiceScope scope)
        where T : IRequest
    {

        var validator = scope.ServiceProvider.GetService<IValidator<T>>();

        if (validator is not null)
        {
            var result = await validator.ValidateAsync(command);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
