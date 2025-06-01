namespace Packages.Mediatr.Contracts.Handlers;

using global::Mediatr.Contracts.Commands;
using Packages.Mediatr.Contracts.Commands;

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : class;

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand>
    where TCommand : ICommand;