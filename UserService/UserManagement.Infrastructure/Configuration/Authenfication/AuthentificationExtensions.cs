namespace UserManagement.Infrastructure.Configuration.Authenfication;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static class AuthentificationExtensions
{
    private const string AuthenticationSigningKey = "DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=";
    private const string AuthenticationAuthorityUrl = "https://localhost:7110";
    private const string AuthenticationAudience = "review-platform-api-gateway";

    public static IServiceCollection ConfigureAuthentification(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.Authority = AuthenticationAuthorityUrl;

                 options.RequireHttpsMetadata = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = AuthenticationAuthorityUrl,
                     ValidAudience = AuthenticationAudience,
                     IssuerSigningKey = new SymmetricSecurityKey(
                                Convert.FromBase64String(AuthenticationSigningKey)),
                 };
                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         return Task.CompletedTask;
                     },
                     OnAuthenticationFailed = context =>
                     {
                         return Task.CompletedTask;
                     },
                 };
             }); ;

        services.AddAuthorization();

        return services;
    }
}
