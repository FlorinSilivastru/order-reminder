namespace UserMangement.Infrastructure.Configuration.Validation;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserMangement.Application;

public static class ValidationRegistration
{
    public static void RegisterValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<MockCommand>();
    }
}
