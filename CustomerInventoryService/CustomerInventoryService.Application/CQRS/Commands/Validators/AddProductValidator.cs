using FluentValidation;

namespace CustomerInventoryService.Application.CQRS.Commands.Validators;

public class AddProductValidator : AbstractValidator<AddProductCommand>
{
    public AddProductValidator()
    {
        this.RuleFor(product => product.Id).NotNull();
    }
}
