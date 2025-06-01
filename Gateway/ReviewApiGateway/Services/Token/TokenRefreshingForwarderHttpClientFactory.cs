namespace GatewayApi.Services.Token;

using OpenIddict.Client;
using Yarp.ReverseProxy.Forwarder;

internal sealed class TokenRefreshingForwarderHttpClientFactory(OpenIddictClientService service)
    : ForwarderHttpClientFactory
{
    private readonly OpenIddictClientService service = service ?? throw new ArgumentNullException(nameof(service));

    protected override HttpMessageHandler WrapHandler(ForwarderHttpClientContext context, HttpMessageHandler handler)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(handler);

        return new TokenRefreshingDelegatingHandler(this.service, handler);
    }
}