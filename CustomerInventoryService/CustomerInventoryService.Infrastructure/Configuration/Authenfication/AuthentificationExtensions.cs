﻿using CustomerInventoryService.Infrastructure.Configuration.Settings.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CustomerInventoryService.Infrastructure.Configuration.Authenfication;

public static class AuthentificationExtensions
{
    public static IServiceCollection ConfigureAuthentification(this IServiceCollection services)
    {
        using var provider = services.BuildServiceProvider();
        var identitySettings = provider.GetRequiredService<IdentityProviderSettings>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.Authority = identitySettings.Uri;

                 options.RequireHttpsMetadata = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = identitySettings.Uri,
                     ValidAudience = identitySettings.Audience,
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
             });

        services.AddAuthorization();

        return services;
    }
}
