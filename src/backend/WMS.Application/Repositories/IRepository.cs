using System.Linq.Expressions;

namespace WMS.Application;

public interface IRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task<T?> GetByIdAsync(Guid id, string includeProperties = "", bool disableTracking = false);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null!, string includeProperties = "", bool disableTracking = false);
    T Update(T entity);
    T[] UpdateRange(params T[] entities);
    void Delete(T entity);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression = null!, string includeProperties = "", bool disableTracking = false);
}
