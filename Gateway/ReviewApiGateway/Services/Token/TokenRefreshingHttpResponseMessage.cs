namespace GatewayApi.Services.Token;

using static OpenIddict.Client.OpenIddictClientModels;

public sealed class TokenRefreshingHttpResponseMessage : HttpResponseMessage
{
    public TokenRefreshingHttpResponseMessage(RefreshTokenAuthenticationResult result, HttpResponseMessage response)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentNullException.ThrowIfNull(result);

        this.RefreshTokenAuthenticationResult = result;

        this.Content = response.Content;
        this.StatusCode = response.StatusCode;
        this.Version = response.Version;

        foreach (var header in response.Headers)
        {
            this.Headers.Add(header.Key, header.Value);
        }
    }

    public RefreshTokenAuthenticationResult RefreshTokenAuthenticationResult { get; }
}