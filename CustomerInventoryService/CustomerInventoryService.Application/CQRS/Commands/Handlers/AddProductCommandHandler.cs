namespace CustomerInventoryService.Application.CQRS.Commands.Handlers;

using Packages.Mediatr.Contracts.Handlers;

public class AddProductCommandHandler
    : ICommandHandler<AddProductCommand>
{
    public Task HandleAsync(AddProductCommand command)
    {
        return Task.CompletedTask;
    }
}
