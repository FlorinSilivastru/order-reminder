namespace GatewayApi.Configurations.Settings;

using GatewayApi.Configurations.Settings.Dtos;

public static class CofigureSettingsExtension
{
    public static IServiceCollection ConfigureApplicationSettings(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.ConfigureSetting<IdentityProviderSettings>(configuration);
        services.ConfigureSetting<LocalIdentityStore>(configuration);
        return services;
    }

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
