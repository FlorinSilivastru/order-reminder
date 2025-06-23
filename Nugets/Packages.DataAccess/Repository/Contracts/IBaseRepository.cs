

namespace Packages.DataAccess.Repository.Contracts
{
    public interface IBaseRepository<TModel>
        where TModel : class
    {
        Task<TModel> AddAsync(TModel entity, CancellationToken cancellationToken = default);

        void Delete(TModel entity);

        Task DeleteByIdAsync(CancellationToken cancellationToken = default,params object[] keyValues);

        Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TModel?> GetByIdAsync(CancellationToken cancellationToken = default, params object[] keyValues);

        TModel Update(TModel entity);
    }
}