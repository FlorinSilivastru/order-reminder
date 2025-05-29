namespace UserManagement.Endpoints;

using Mediatr.Contracts.Services;

internal static class Endpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        MapOpenApiInDevEnvironment(app);

        app.MapGet("/add", () => Results.Ok("Test middlewares"))
        .WithName("Add-User");
    }

    private static void MapOpenApiInDevEnvironment(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
    }
}
