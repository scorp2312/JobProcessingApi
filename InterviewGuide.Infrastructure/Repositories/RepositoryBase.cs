namespace InterviewGuide.Infrastructure.Repositories;

using InterviewGuide.Domain.Interfaces;
using InterviewGuide.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public abstract class RepositoryBase<TEntity, TKey>(ApplicationDbContext context)
    : IRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await this.dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity?> GetAsync(TKey id)
    {
        return await this.dbSet.FindAsync(id)
               ?? throw new ApplicationException();
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await this.dbSet.ToListAsync();
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        return await context.SaveChangesAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity)
    {
        this.dbSet.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}