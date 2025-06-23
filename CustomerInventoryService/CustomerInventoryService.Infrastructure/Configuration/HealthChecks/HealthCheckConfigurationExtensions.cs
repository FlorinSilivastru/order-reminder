using CustomerInventoryService.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerInventoryService.Infrastructure.Configuration.HealthChecks;

public static class HealthCheckConfigurationExtensions
{
    public static IServiceCollection AddHealthCheckConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString(nameof(ProductDbContext)), name: "Database Connection")
            .AddDbContextCheck<ProductDbContext>("Entity Framework Database Connection")
            .AddCheck<UserApiHealthCheck>("Enterprise Connection");

        return services;
    }
}
