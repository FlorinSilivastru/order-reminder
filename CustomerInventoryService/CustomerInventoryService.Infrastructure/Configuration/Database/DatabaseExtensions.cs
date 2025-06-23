using CustomerInventoryService.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerInventoryService.Infrastructure.Configuration.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString(nameof(ProductDbContext))
                ?? throw new InvalidOperationException($"Connection string {nameof(ProductDbContext)} not found.");
            options.UseSqlServer(connectionString);
        });
        return services;
    }
}
