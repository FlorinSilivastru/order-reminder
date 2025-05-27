using CustomerInventoryService.Application.Interfaces;
using MessagingContracts;

namespace CustomerInventoryService.Endpoints;

internal static class Endpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        MapOpenApiInDevEnvironment(app);

        app.MapGet("/weatherforecast", async (IServicebus bus) =>
        {
            await bus.Publish<SendReminderMessage>(new
            {
                OrderId = Guid.NewGuid(),
            });
        })
        .WithName("GetWeatherForecast");
    }

    private static void MapOpenApiInDevEnvironment(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
    }
}
