using Microsoft.Extensions.DependencyInjection;

namespace CustomerInventoryService.Infrastructure.Configuration.Authorization;

public static class AuthorizationExtensions
{
    public const string CustomerOnlyPolicyName = "CustomerOnly";
    private const string BasicUserRole = "BasicUser";

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(CustomerOnlyPolicyName, policy =>
                policy.RequireRole(BasicUserRole));
        });
        return services;
    }
}
