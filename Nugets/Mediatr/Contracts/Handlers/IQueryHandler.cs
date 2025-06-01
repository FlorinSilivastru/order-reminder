namespace Packages.Mediatr.Contracts.Handlers;

using global::Mediatr.Contracts.Queries;
using Packages.Mediatr.Contracts.Queries;

public interface IQueryHandler<T, TResponse>
    : IRequestHandler<T, TResponse>
    where T : IQuery<TResponse>
    where TResponse : class;
