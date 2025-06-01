using Packages.Mediatr.Contracts.Common;

namespace Packages.Mediatr.Contracts.Queries;

public interface IQuery<TResponse>
    : IRequest<TResponse>
    where TResponse : class;
