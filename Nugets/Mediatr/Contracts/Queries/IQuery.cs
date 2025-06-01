namespace Packages.Mediatr.Contracts.Queries;

using global::Mediatr.Contracts.Common;
using Packages.Mediatr.Contracts.Common;

public interface IQuery<TResponse>
    : IRequest<TResponse>
    where TResponse : class;
