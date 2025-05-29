namespace CustomerInventoryService.Application.CQRS.Commands.Validators;

using FluentValidation;

public class AddProductValidator : AbstractValidator<AddProductCommand>
{
    public AddProductValidator()
    {
        this.RuleFor(product => product.Id).NotNull();
    }
}
