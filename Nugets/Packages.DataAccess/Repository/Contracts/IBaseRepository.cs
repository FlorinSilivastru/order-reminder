namespace Packages.DataAccess.Repository.Contracts
{
    public interface IBaseRepository<TModel> where TModel : class
    {
        Task AddAsync(TModel entity);
        Task DeleteAsync(TModel entity);
        Task DeleteByIdAsync(params object[] keyValues);
        Task<List<TModel>> GetAllAsync();
        Task<TModel?> GetByIdAsync(params object[] keyValues);
        Task UpdateAsync(TModel entity);
    }
}