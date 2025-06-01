using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application;

namespace UserService.Infrastructure.Configuration.Validation;

public static class ValidationRegistration
{
    public static IServiceCollection RegisterValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<MockCommand>();
        return services;
    }
}
