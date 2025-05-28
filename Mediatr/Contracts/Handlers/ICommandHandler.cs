using Mediatr.Contracts.Commands;

namespace Mediatr.Contracts.Handlers;

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : class;

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand>
    where TCommand : ICommand;