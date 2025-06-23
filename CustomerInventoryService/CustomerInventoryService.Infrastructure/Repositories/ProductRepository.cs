using CustomerInventoryService.Application.Contracts;
using CustomerInventoryService.Infrastructure.Persistence.Contexts;
using Packages.DataAccess.Repository;

namespace CustomerInventoryService.Infrastructure.Repositories;

public class ProductRepository<T>(ProductDbContext productDbContext)
    : BaseRepository<T>(productDbContext), IProductRepository<T>
    where T : class
{
}