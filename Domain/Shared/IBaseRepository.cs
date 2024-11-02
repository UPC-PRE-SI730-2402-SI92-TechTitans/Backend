namespace Domain.Shared;

public interface IBaseRepository<TEntity>
{
    Task AddAsync(TEntity entity);
    Task<TEntity> FindByIdAsync(int id);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);

    Task<IEnumerable<TEntity>> ListAsync();
}