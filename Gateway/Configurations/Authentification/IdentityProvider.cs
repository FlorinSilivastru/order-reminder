namespace GatewayApi.Configurations.Authentification;

using GatewayApi.Configurations.Settings.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using OpenIddict.Client;
using static OpenIddict.Abstractions.OpenIddictConstants;

public static class IdentityProvider
{
    public static IServiceCollection SetupIdentityProviderAuthentication(this IServiceCollection services)
    {
        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var identitySettings = scope.ServiceProvider.GetRequiredService<IdentityProviderSettings>();

        services.AddSingleton(s =>
        {
            var localIdentityStore = s.GetRequiredService<LocalIdentityStore>();
#pragma warning disable IDISP004 // Don't ignore created IDisposable
            return new MongoClient(localIdentityStore.MongoDbConnectionString)
                    .GetDatabase(localIdentityStore.IdentityProviderDatabaseName);
#pragma warning restore IDISP004 // Don't ignore created IDisposable
        });

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

                ConfigureClientRegistration(options, identitySettings);
            })
            .AddValidation(options =>
            {
                options.SetIssuer(identitySettings.Uri);

                options.AddEncryptionKey(new SymmetricSecurityKey(
                                                Convert.FromBase64String(identitySettings.SymmetricEncryptionKey)));

                options.AddAudiences(identitySettings.ClientId);

                options.UseSystemNetHttp();

                options.UseAspNetCore();
            });
        return services;
    }

    private static void ConfigureClientRegistration(OpenIddictClientBuilder options, IdentityProviderSettings identitySettings)
    {
        // Add a client registration matching the client application definition in the server project.
        options.AddRegistration(new OpenIddictClientRegistration
        {
            Issuer = new Uri(identitySettings.Uri, UriKind.Absolute),
            ClientId = identitySettings.ClientId,
            ClientSecret = identitySettings.ClientSecret,
            Scopes = { Scopes.Email, Scopes.Profile, identitySettings.Scope },

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
