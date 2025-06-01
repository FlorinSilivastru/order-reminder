namespace UserManagement.Endpoints;

using Asp.Versioning;
using UserManagement.Infrastructure.Configuration.Authorization;

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
