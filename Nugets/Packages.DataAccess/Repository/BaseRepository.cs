using Microsoft.EntityFrameworkCore;
using Packages.DataAccess.Repository.Contracts;

namespace Packages.DataAccess.Repository;

public class BaseRepository<TModel> : IBaseRepository<TModel>
    where TModel : class
{
    protected readonly DbContext Context;

    protected readonly DbSet<TModel> DbSet;

    public BaseRepository(DbContext dbContext)
    {
        this.Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.DbSet = dbContext.Set<TModel>();
    }

    public virtual async Task AddAsync(TModel entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await this.DbSet.AddAsync(entity);
        await this.Context.SaveChangesAsync();
    }

    public virtual async Task<TModel?> GetByIdAsync(params object[] keyValues)
    {
        return await this.DbSet.FindAsync(keyValues);
    }

    public virtual async Task<List<TModel>> GetAllAsync()
    {
        return await this.DbSet.ToListAsync();
    }

    public virtual async Task UpdateAsync(TModel entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        this.DbSet.Update(entity);
        await this.Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TModel entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        this.DbSet.Remove(entity);
        await this.Context.SaveChangesAsync();
    }

    public virtual async Task DeleteByIdAsync(params object[] keyValues)
    {
        var entity = await this.GetByIdAsync(keyValues);
        if (entity != null)
        {
            this.DbSet.Remove(entity);
            await this.Context.SaveChangesAsync();
        }
    }
}
