namespace CustomerInventoryService.Infrastructure.Configuration.Authorization;

using Microsoft.Extensions.DependencyInjection;

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
