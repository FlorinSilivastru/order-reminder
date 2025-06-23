using Packages.DataAccess.Repository.Contracts;

namespace CustomerInventoryService.Application.Contracts;

public interface IProductRepository<T> : IBaseRepository<T>
    where T : class
{
}
