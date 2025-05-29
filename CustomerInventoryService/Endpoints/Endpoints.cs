namespace CustomerInventoryService.Endpoints;

using CustomerInventoryService.Application.CQRS.Commands;
using CustomerInventoryService.Application.CQRS.Queries;
using CustomerInventoryService.Application.CQRS.Queries.Dtos;
using CustomerInventoryService.Application.Interfaces;
using Mediatr.Contracts.Services;
using Messaging.Masstransit.Contracts;

internal static class Endpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        MapOpenApiInDevEnvironment(app);

        app.MapGet("/getOrderDetails", async (
                [AsParameters] GetOrderDetails query,
                IMediatr mediatr)
                => Results.Ok(await mediatr.SendAsync<GetOrderDetails, OrderDetailsDto>(query)))
        .WithName("Get-Order-Details");

        app.MapPost("/add-product", async (
            AddProductCommand command,
            IServicebus bus,
            IMediatr mediatr) =>
            {
                await mediatr.SendAsync(command);
                await bus.Publish<SendReminderMessage>(new
                {
                    OrderId = Guid.NewGuid(),
                });
            })
        .WithName("Add-Product");
    }

    private static void MapOpenApiInDevEnvironment(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
    }
}
