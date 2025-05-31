namespace GatewayApi.Configurations.Authentification;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Client;
using static OpenIddict.Abstractions.OpenIddictConstants;

public static class IdentityProvider
{
    private const string SymmetricEncryptionKey = "DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=";
    private const string IdentityProviderUri = "https://localhost:7110";
    private const string ApiGatewayClientId = "review-platform-api-gateway";
    private const string ApiGatewayClientSecret = "847862D0-DEF9-4215-A99D-86E6B8DAB342";
    private const string ApiGatewayScope = "review-api-gateway-scope";

    public static IServiceCollection SetupIdentityProviderAuthentication(this IServiceCollection services)
    {
        //services.AddSingleton(s =>
        //{
        //    return new MongoClient("").GetDatabase("IdentityProvider-local");
        //});

        services
            .AddOpenIddict()
            .AddCore(options =>
            {
                options.UseMongoDb();
            })
            .AddClient(options =>
            {
                options.AllowAuthorizationCodeFlow();

                ConfigureClientCertificates(options);

                ConfigureAspNetCoreIntegration(options);

                options.UseSystemNetHttp();

                ConfigureClientRegistration(options);
            })
            .AddValidation(options =>
            {
                options.SetIssuer(IdentityProviderUri);

                options.AddEncryptionKey(new SymmetricSecurityKey(
                                Convert.FromBase64String(SymmetricEncryptionKey)));

                options.AddAudiences(ApiGatewayClientId);

                options.UseSystemNetHttp();

                options.UseAspNetCore();
            });
        return services;
    }

    private static void ConfigureClientRegistration(OpenIddictClientBuilder options)
    {
        // Add a client registration matching the client application definition in the server project.
        options.AddRegistration(new OpenIddictClientRegistration
        {
            Issuer = new Uri(IdentityProviderUri, UriKind.Absolute),
            ClientId = ApiGatewayClientId,
            ClientSecret = ApiGatewayClientSecret,
            Scopes = { Scopes.Email, Scopes.Profile, ApiGatewayScope },

            // Note: to mitigate mix-up attacks, it's recommended to use a unique redirection endpoint
            // URI per provider, unless all the registered providers support returning a special "iss"
            // parameter containing their URL as part of authorization responses. For more information,
            // see https://datatracker.ietf.org/doc/html/draft-ietf-oauth-security-topics#section-4.4.
            RedirectUri = new Uri("callback/login/local", UriKind.Relative),
            PostLogoutRedirectUri = new Uri("callback/logout/local", UriKind.Relative),
        });
    }

    private static void ConfigureAspNetCoreIntegration(OpenIddictClientBuilder options)
    {
        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        options.UseAspNetCore()
               .EnableStatusCodePagesIntegration()
               .EnableRedirectionEndpointPassthrough()
               .EnablePostLogoutRedirectionEndpointPassthrough();
    }

    private static void ConfigureClientCertificates(OpenIddictClientBuilder options)
    {
        // Register the signing and encryption credentials used to protect
        // sensitive data like the state tokens produced by OpenIddict.
        options.AddDevelopmentEncryptionCertificate()
               .AddDevelopmentSigningCertificate();
    }
}
