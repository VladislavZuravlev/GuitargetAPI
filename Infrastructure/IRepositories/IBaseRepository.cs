

namespace Infrastructure.IRepositories;

public interface IBaseRepository<TEntity>
{
    Task<List<TEntity>> GetEntityAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null);
    Task<bool> InsertEntityAsync(TEntity entity);
}