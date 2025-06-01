using Packages.Mediatr.Contracts.Handlers;

namespace CustomerInventoryService.Application.CQRS.Commands.Handlers;

public class AddProductCommandHandler
    : ICommandHandler<AddProductCommand>
{
    public Task HandleAsync(AddProductCommand command)
    {
        return Task.CompletedTask;
    }
}
