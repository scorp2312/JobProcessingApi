namespace InterviewGuide.Domain.Interfaces;

public interface IRepository<TEntity, TKey>
{
    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity?> GetAsync(TKey id);

    Task<List<TEntity>> GetAllAsync();

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> DeleteAsync(TEntity entity);
}