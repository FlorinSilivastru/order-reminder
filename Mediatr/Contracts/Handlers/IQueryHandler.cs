using Mediatr.Contracts.Queries;

namespace Mediatr.Contracts.Handlers;

public interface IQueryHandler<T, TResponse>
    : IRequestHandler<T, TResponse>
    where T : IQuery<TResponse>
    where TResponse : class;
