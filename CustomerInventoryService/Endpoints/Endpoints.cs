﻿using Asp.Versioning;
using CustomerInventoryService.Application.CQRS.Commands;
using CustomerInventoryService.Application.CQRS.Queries;
using CustomerInventoryService.Application.CQRS.Queries.Dtos;
using CustomerInventoryService.Infrastructure.Configuration.Authorization;
using Packages.Mediatr.Contracts.Services;
using Packages.Messaging.Masstransit.Contracts;
using Packages.Messaging.Masstransit.Services;

namespace CustomerInventoryServiceApi.Endpoints;

internal static class Endpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/healthCheck");

        app.MapOpenApiInDevEnvironment();

        app.MapCustomerServiceEndpoints();
    }

    private static void MapCustomerServiceEndpoints(this WebApplication app)
    {
        var versionSet = app.NewApiVersionSet()
        .HasApiVersion(new ApiVersion(1, 0))
        .HasApiVersion(new ApiVersion(2, 0))
        .ReportApiVersions()
        .Build();

        app.MapGet("/api/v{version:apiVersion}/customer-service/getOrderDetails", async (
                [AsParameters] GetOrderDetails query,
                IMediatr mediatr)
                => Results.Ok(await mediatr.SendAsync<GetOrderDetails, OrderDetailsDto>(query)))
        .WithName("Get-Order-Details")
        .WithApiVersionSet(versionSet)
        .MapToApiVersion(new ApiVersion(1, 0));
        //.RequireAuthorization(AuthorizationExtensions.CustomerOnlyPolicyName);

        app.MapPost("/api/v{version:apiVersion}/customer-service/add-product", async (
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
        .MapToApiVersion(new ApiVersion(1, 0))
        .RequireAuthorization(AuthorizationExtensions.CustomerOnlyPolicyName);
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
