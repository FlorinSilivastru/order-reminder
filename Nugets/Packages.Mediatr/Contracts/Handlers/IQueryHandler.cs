using Packages.Mediatr.Contracts.Queries;

namespace Packages.Mediatr.Contracts.Handlers;

public interface IQueryHandler<T, TResponse>
    : IRequestHandler<T, TResponse>
    where T : IQuery<TResponse>
    where TResponse : class;
