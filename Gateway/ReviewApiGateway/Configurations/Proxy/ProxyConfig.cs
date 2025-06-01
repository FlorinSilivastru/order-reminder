namespace GatewayApi.Configurations.Proxy;

using System.Globalization;
using GatewayApi.Services.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;
using static OpenIddict.Client.AspNetCore.OpenIddictClientAspNetCoreConstants;
using static OpenIddict.Client.OpenIddictClientModels;

public static class ProxyConfig
{
    public static void ConfigureProxy(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"))
            .AddTransforms(builder =>
            {
                builder.AddRequestTransform(async context =>
                {
                    // Attach the access token, access token expiration date and refresh token resolved from the authentication
                    // cookie to the request options so they can later be resolved from the delegating handler and attached
                    // to the request message or used to refresh the tokens if the server returned a 401 error response.
                    //
                    // Alternatively, the user tokens could be stored in a database or a distributed cache.

                    var result = await context.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    context.ProxyRequest.Options.Set(
                        key: new(Tokens.BackchannelIdentityToken),
                        value: result?.Properties?.GetTokenValue(Tokens.BackchannelIdentityToken));

                    context.ProxyRequest.Options.Set(
                        key: new(Tokens.BackchannelAccessToken),
                        value: result?.Properties?.GetTokenValue(Tokens.BackchannelAccessToken));

                    context.ProxyRequest.Options.Set(
                        key: new(Tokens.BackchannelAccessTokenExpirationDate),
                        value: result?.Properties?.GetTokenValue(Tokens.BackchannelAccessTokenExpirationDate));

                    context.ProxyRequest.Options.Set(
                        key: new(Tokens.RefreshToken),
                        value: result?.Properties?.GetTokenValue(Tokens.RefreshToken));
                });

                builder.AddResponseTransform(async context =>
                {
                    // If tokens were refreshed during the request handling (e.g due to the stored access token being
                    // expired or a 401 error response being returned by the resource server), extract and attach them
                    // to the authentication cookie that will be returned to the browser: doing that is essential as
                    // OpenIddict uses rolling refresh tokens: if the refresh token wasn't replaced, future refresh
                    // token requests would end up being rejected as they would be treated as replayed requests.

                    if (context.ProxyResponse is not TokenRefreshingHttpResponseMessage
                        {
                            RefreshTokenAuthenticationResult: RefreshTokenAuthenticationResult
                        } response)
                    {
                        return;
                    }

                    var result = await context.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    // Override the tokens using the values returned in the token response.
                    var properties = result?.Properties?.Clone();
                    properties?.UpdateTokenValue(Tokens.BackchannelAccessToken, response.RefreshTokenAuthenticationResult.AccessToken);

                    properties?.UpdateTokenValue(Tokens.BackchannelAccessTokenExpirationDate,
                        response?.RefreshTokenAuthenticationResult?.AccessTokenExpirationDate?.ToString(CultureInfo.InvariantCulture) ?? "");

                    // Note: if no refresh token was returned, preserve the refresh token initially returned.
                    if (!string.IsNullOrEmpty(response?.RefreshTokenAuthenticationResult.RefreshToken))
                    {
                        properties?.UpdateTokenValue(Tokens.RefreshToken, response.RefreshTokenAuthenticationResult.RefreshToken);
                    }

                    // Remove the redirect URI from the authentication properties
                    // to prevent the cookies handler from genering a 302 response.
                    properties.RedirectUri = null;

                    // Note: this event handler can be called concurrently for the same user if multiple HTTP
                    // responses are returned in parallel: in this case, the browser will always store the latest
                    // cookie received and the refresh tokens stored in the other cookies will be discarded.
                    await context.HttpContext.SignInAsync(result?.Ticket?.AuthenticationScheme, result?.Principal, properties);
                });
            });

        // Replace the default HTTP client factory used by YARP by an instance able to inject the HTTP delegating
        // handler that will be used to attach the access tokens to HTTP requests or refresh tokens if necessary.
        services.Replace(ServiceDescriptor.Singleton<IForwarderHttpClientFactory, TokenRefreshingForwarderHttpClientFactory>());
    }
}
