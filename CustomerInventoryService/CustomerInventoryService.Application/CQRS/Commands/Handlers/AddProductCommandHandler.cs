namespace CustomerInventoryService.Application.CQRS.Commands.Handlers;

using Mediatr.Contracts.Handlers;

public class AddProductCommandHandler
    : ICommandHandler<AddProductCommand>
{
    public Task HandleAsync(AddProductCommand command)
    {
        return Task.CompletedTask;
    }
}
