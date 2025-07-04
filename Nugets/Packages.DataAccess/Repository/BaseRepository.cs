﻿using Microsoft.EntityFrameworkCore;
using Packages.DataAccess.Repository.Contracts;

namespace Packages.DataAccess.Repository;

public class BaseRepository<TModel>(DbContext dbContext) : IBaseRepository<TModel>
    where TModel : class
{
    protected readonly DbContext Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    protected readonly DbSet<TModel> DbSet = dbContext.Set<TModel>();

    public virtual async Task<TModel> AddAsync(
        TModel entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        var result = await this.DbSet.AddAsync(entity);
        return result.Entity;
    }

    public virtual async Task<TModel?> GetByIdAsync(
        CancellationToken cancellationToken = default,
        params object[] keyValues)
    {
        return await this.DbSet.FindAsync(keyValues, cancellationToken);
    }

    public virtual async Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await this.DbSet.ToListAsync(cancellationToken);
    }

    public virtual TModel Update(TModel entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var result = this.DbSet.Update(entity);
        return result.Entity;
    }

    public virtual void Delete(TModel entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity is ISoftDelete softDeletable)
        {
            softDeletable.IsDeleted = true;
            this.Update(entity);
        }
        else
        {
            this.DbSet.Remove(entity);
        }
    }

    public virtual async Task DeleteByIdAsync(
        CancellationToken cancellationToken = default,
        params object[] keyValues)
    {
        var entity = await this.GetByIdAsync(cancellationToken, keyValues);
        if (entity != null)
        {
            this.DbSet.Remove(entity);
        }
    }
}
