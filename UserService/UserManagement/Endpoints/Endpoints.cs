﻿using Asp.Versioning;
using UserService.Infrastructure.Configuration.Authorization;

namespace UserServiceApi.Endpoints;

internal static class Endpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/healthCheck");

        app.MapOpenApiInDevEnvironment();

        app.ConfigureUserServiceRoutes();
    }

    private static void ConfigureUserServiceRoutes(this WebApplication app)
    {
        var versionSet = app.NewApiVersionSet()
        .HasApiVersion(new ApiVersion(1, 0))
        .HasApiVersion(new ApiVersion(2, 0))
        .ReportApiVersions()
        .Build();

        app.MapGet("/api/v{version:apiVersion}/users-service/login", () => Results.Ok("Test middlewares"))
       .WithName("login")
       .WithApiVersionSet(versionSet)
       .MapToApiVersion(new ApiVersion(1, 0));

        app.MapGet("/api/v{version:apiVersion}/users-service/add", () => Results.Ok("Test middlewares"))
        .WithName("Add-User")
        .WithApiVersionSet(versionSet)
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization(AuthorizationExtensions.AdminOnlyPolicyName);
    }

    private static void MapOpenApiInDevEnvironment(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
