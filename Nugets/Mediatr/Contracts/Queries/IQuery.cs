namespace Packages.Mediatr.Contracts.Queries;

using Packages.Mediatr.Contracts.Common;

public interface IQuery<TResponse>
    : IRequest<TResponse>
    where TResponse : class;
