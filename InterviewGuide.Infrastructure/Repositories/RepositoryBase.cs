namespace InterviewGuide.Infrastructure.Repositories;

using InterviewGuide.Domain.Exceptions;
using InterviewGuide.Domain.Interfaces;
using InterviewGuide.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class RepositoryBase<TEntity, TKey>(ApplicationDbContext context)
    : IRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await this.dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> GetAsync(TKey id)
    {
        return await this.dbSet.FindAsync(id)
               ?? throw new NotFoundException<TKey>(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await this.dbSet.ToListAsync();
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        this.dbSet.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        this.dbSet.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}