using Packages.Mediatr.Contracts.Common;

namespace Packages.Mediatr.Contracts.Commands;

public interface ICommand : IRequest;

public interface ICommand<TResponse>
    : IRequest<TResponse>
    where TResponse : class;
