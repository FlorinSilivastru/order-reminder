namespace CustomerInventoryService.Endpoints;

using Asp.Versioning;
using CustomerInventoryService.Application.CQRS.Commands;
using CustomerInventoryService.Application.CQRS.Queries;
using CustomerInventoryService.Application.CQRS.Queries.Dtos;
using Mediatr.Contracts.Services;
using Messaging.Masstransit.Contracts;
using Messaging.Masstransit.Services;

internal static class Endpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        MapOpenApiInDevEnvironment(app);


        var versionSet = app.NewApiVersionSet()
        .HasApiVersion(new ApiVersion(1, 0))
        .HasApiVersion(new ApiVersion(2, 0))
        .ReportApiVersions()
        .Build();

        app.MapGet("/api/v{version:apiVersion}/getOrderDetails", async (
                [AsParameters] GetOrderDetails query,
                IMediatr mediatr)
                => Results.Ok(await mediatr.SendAsync<GetOrderDetails, OrderDetailsDto>(query)))
        .WithName("Get-Order-Details")
        .WithApiVersionSet(versionSet)
        .MapToApiVersion(new ApiVersion(1, 0));

        app.MapPost("/api/v{version:apiVersion}/add-product", async (
            AddProductCommand command,
            IEventBus bus,
            IMediatr mediatr) =>
            {
                await mediatr.SendAsync(command);
                await bus.PublishAsync(new SendReminderMessage
                {
                    OrderId = Guid.NewGuid(),
                });
            })
        .WithName("Add-Product")
        .WithApiVersionSet(versionSet)
        .MapToApiVersion(new ApiVersion(1, 0));
    }

    private static void MapOpenApiInDevEnvironment(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
