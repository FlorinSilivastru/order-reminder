using Packages.Mediatr.Contracts.Common;

namespace Packages.Mediatr.Contracts.Handlers;

public interface IRequestHandler<TCommand>
    where TCommand : IRequest
{
    Task HandleAsync(TCommand command);
}

public interface IRequestHandler<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
    where TResponse : class
{
    Task<TResponse> HandleAsync(TCommand command);
}
