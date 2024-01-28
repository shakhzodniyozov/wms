using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class GenericRepository<T> : IRepository<T> where T : class, IEntity
{
    public GenericRepository(IApplicationDbContext dbContext)
    {
        context = dbContext;
        dbSet = dbContext.Set<T>();
    }

    protected readonly IApplicationDbContext context;
    protected readonly DbSet<T> dbSet;

    public async Task<T> CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        return entity;
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression = null!, string includeProperties = "", bool disableTracking = false)
    {
        IQueryable<T> query = dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (expression != null)
            return await query.FirstOrDefaultAsync(expression);

        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null!, string includeProperties = "", bool disableTracking = false)
    {
        IQueryable<T> query = dbSet;

        if (expression != null)
            query = query.Where(expression);

        if (disableTracking)
            query = query.AsNoTracking();

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, string includeProperties = "", bool disableTracking = false)
    {
        return await FirstOrDefaultAsync(x => x.Id == id, includeProperties, disableTracking);
    }

    public virtual T Update(T entity)
    {
        dbSet.Update(entity);
        return entity;
    }

    public virtual T[] UpdateRange(params T[] entities)
    {
        dbSet.UpdateRange(entities);
        return entities;
    }
}
