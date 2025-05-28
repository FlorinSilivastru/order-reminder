namespace Mediatr.Contracts.Common;

public interface IRequest;

public interface IRequest<TResponse>
    where TResponse : class;
