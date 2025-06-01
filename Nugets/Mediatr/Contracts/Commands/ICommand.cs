namespace Mediatr.Contracts.Commands;

using global::Mediatr.Contracts.Common;

public interface ICommand : IRequest;

public interface ICommand<TResponse>
    : IRequest<TResponse>
    where TResponse : class;
