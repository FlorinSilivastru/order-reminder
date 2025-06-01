namespace GatewayApi.Configurations.Authentification;

using Microsoft.AspNetCore.Authentication.Cookies;

public static class CookieAuth
{
    public static IServiceCollection SetupCookieAuthenticationn(this IServiceCollection services)
    {
        services.ConfigureCookieAuthentication();
        services.SetupCookieAuthenticationPolicy();

        return services;
    }

    private static void SetupCookieAuthenticationPolicy(this IServiceCollection services)
    {
        // Create an authorization policy used by YARP when forwarding requests
        // from the gateway to the apis resource server.
        services.AddAuthorization(options => options.AddPolicy("CookieAuthenticationPolicy", builder =>
        {
            builder.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
            builder.RequireAuthenticatedUser();
        }));
    }

    private static void ConfigureCookieAuthentication(this IServiceCollection services)
    {
        // Configure Authentication with Cookies
        // Configure the antiforgery stack to allow extracting
        // antiforgery tokens from the X-XSRF-TOKEN header.
        services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-XSRF-TOKEN";
            options.Cookie.Name = "__Host-X-XSRF-TOKEN";
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/api/v1/identity/Authentification/login";
            options.LogoutPath = "/api/v1/identity/Authentification/logout";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
            options.SlidingExpiration = false;
        });
    }
}
