
namespace Mediatr.Contracts.Handlers;

using global::Mediatr.Contracts.Common;

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
