using Packages.Mediatr.Contracts.Commands;

namespace CustomerInventoryService.Application.CQRS.Commands;

public record class AddProductCommand
    : ICommand
{
    public string? Id { get; init; }
}
