namespace CustomerInventoryService.Application.CQRS.Queries.Handlers;

using CustomerInventoryService.Application.CQRS.Queries.Dtos;
using Packages.Mediatr.Contracts.Handlers;

public class GetOrderDetailsHandler
    : IQueryHandler<GetOrderDetails, OrderDetailsDto>
{
    public async Task<OrderDetailsDto> HandleAsync(GetOrderDetails command)
    {
        await Task.FromResult(0);
        return new OrderDetailsDto(Guid.NewGuid(), DateTime.UtcNow);
    }
}
