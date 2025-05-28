namespace CustomerInventoryService.Application.CQRS.Queries;

using CustomerInventoryService.Application.CQRS.Queries.Dtos;
using Mediatr.Contracts.Queries;

public record class GetOrderDetails
    : IQuery<OrderDetailsDto>
{
    public string? Id { get; init; }
}
