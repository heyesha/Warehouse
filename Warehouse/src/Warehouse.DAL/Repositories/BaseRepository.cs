using Warehouse.Domain.Interfaces.Repositories;

namespace Warehouse.DAL.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Entity is NULL");
        }
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Entity is NULL");
        }
        _dbContext.Update(entity);

        return entity;
    }

    public void Remove(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Entity is NULL");
        }
        _dbContext.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}