using Mediatr.Contracts.Common;

namespace Mediatr.Contracts.Commands;

public interface ICommand : IRequest;

public interface ICommand<TResponse>
    : IRequest<TResponse>
    where TResponse : class;
