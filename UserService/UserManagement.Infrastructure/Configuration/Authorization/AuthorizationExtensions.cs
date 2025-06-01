namespace UserService.Infrastructure.Configuration.Authorization;

using Microsoft.Extensions.DependencyInjection;

public static class AuthorizationExtensions
{
    public const string AdminOnlyPolicyName = "AdminOnly";

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AdminOnlyPolicyName, policy =>
                policy.RequireRole("Admin"));
        });
        return services;
    }
}
