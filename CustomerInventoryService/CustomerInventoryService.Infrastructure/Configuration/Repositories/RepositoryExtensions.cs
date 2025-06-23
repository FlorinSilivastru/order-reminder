using CustomerInventoryService.Application.Contracts;
using CustomerInventoryService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerInventoryService.Infrastructure.Configuration.Repositories;

public static class RepositoryExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));
        return services;
    }
}
