using Mediatr.Contracts.Common;

namespace Mediatr.Contracts.Queries;

public interface IQuery<TResponse>
    : IRequest<TResponse>
    where TResponse : class;
