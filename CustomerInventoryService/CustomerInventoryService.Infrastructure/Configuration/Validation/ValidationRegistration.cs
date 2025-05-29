namespace CustomerInventoryService.Infrastructure.Configuration.Validation;

using CustomerInventoryService.Application.CQRS.Commands.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class ValidationRegistration
{
    public static void RegisterValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddProductValidator>();
    }
}
