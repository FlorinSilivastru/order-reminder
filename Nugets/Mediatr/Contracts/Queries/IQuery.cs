namespace Mediatr.Contracts.Queries;

using global::Mediatr.Contracts.Common;

public interface IQuery<TResponse>
    : IRequest<TResponse>
    where TResponse : class;
