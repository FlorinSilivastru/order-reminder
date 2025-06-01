using CustomerInventoryService.Application.CQRS.Commands.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerInventoryService.Infrastructure.Configuration.Validation;

public static class ValidationRegistration
{
    public static IServiceCollection RegisterValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddProductValidator>();
        return services;
    }
}
