using CustomerInventoryService.Infrastructure.Configuration.Settings.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerInventoryService.Infrastructure.Configuration.Settings;

public static class CofigureSettingsExtension
{
    public static IServiceCollection ConfigureApplicationSettings(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.ConfigureSetting<IdentityProviderSettings>(configuration);
        return services;
    }

    // TO DO extract this in a package
    private static IServiceCollection ConfigureSetting<T>(
        this IServiceCollection services,
        IConfiguration configuration)
        where T : class
    {
        var identityProviderSettings = configuration
         .GetSection(typeof(T).Name)
         .Get<T>()
         ?? throw new ArgumentException($"Please add the {nameof(T)} in config file!");

        services.AddSingleton(identityProviderSettings);

        return services;
    }
}
