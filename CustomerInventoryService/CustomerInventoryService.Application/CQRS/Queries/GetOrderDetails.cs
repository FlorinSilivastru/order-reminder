using CustomerInventoryService.Application.CQRS.Queries.Dtos;
using Packages.Mediatr.Contracts.Queries;

namespace CustomerInventoryService.Application.CQRS.Queries;

public record class GetOrderDetails
    : IQuery<OrderDetailsDto>
{
    public string? Id { get; init; }
}
