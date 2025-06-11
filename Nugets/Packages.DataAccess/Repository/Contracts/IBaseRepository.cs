

namespace Packages.DataAccess.Repository.Contracts
{
    public interface IBaseRepository<TModel>
        where TModel : class
    {
        Task<TModel> AddAsync(TModel entity);

        void Delete(TModel entity);

        Task DeleteByIdAsync(params object[] keyValues);

        Task<List<TModel>> GetAllAsync();

        Task<TModel?> GetByIdAsync(params object[] keyValues);

        TModel Update(TModel entity);
    }
}