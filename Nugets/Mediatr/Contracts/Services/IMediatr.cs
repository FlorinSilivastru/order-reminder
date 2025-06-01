namespace Packages.Mediatr.Contracts.Services
{
    using global::Mediatr.Contracts.Common;
    using Packages.Mediatr.Contracts.Common;

    public interface IMediatr
    {
        Task SendAsync<T>(T command)
            where T : IRequest;

        Task<TResult> SendAsync<T, TResult>(T query)
            where T : IRequest<TResult>
            where TResult : class;
    }
}
