namespace CustomerInventoryService.Application.CQRS.Commands;

using Packages.Mediatr.Contracts.Commands;

public record class AddProductCommand
    : ICommand
{
    public string? Id { get; init; }
}
