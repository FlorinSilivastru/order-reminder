namespace Packages.Mediatr.Contracts.Commands;

using Packages.Mediatr.Contracts.Common;

public interface ICommand : IRequest;

public interface ICommand<TResponse>
    : IRequest<TResponse>
    where TResponse : class;
