using Infrastructure.Contexts;
using Infrastructure.Helpers;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>: IBaseRepository<TEntity>
where TEntity: class
{
    
    protected readonly PostgresDbContext _ctx;
    protected DbSet<TEntity> _dbSet;

    public BaseRepository(PostgresDbContext ctx)
    {
        _ctx = ctx;
        _dbSet = ctx.Set<TEntity>();
    }
    
    protected virtual IQueryable<TEntity> GetDbSet()
    {
        return _dbSet;
    }
    
    public async Task<List<TEntity>> GetEntityAsync(IEnumerable<Tuple<string, string, object>>? filters = null, string? includeProperties = null, Dictionary<string, string>? orderCollection = null)
    {
        IQueryable <TEntity> query = GetDbSet();
        
        DataFilterHelper.ApplyFilterCollection(filters, ref query);
        DataFilterHelper.ApplyOrdering(orderCollection, ref query);

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.ToListAsync();
    }
    
    public async Task<bool> InsertEntityAsync(TEntity entity)
    {
        int savedCount;

        try
        {
            await _dbSet.AddAsync(entity);
            savedCount = await _ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return false;
        }
        
        return savedCount > 0;
    }
}