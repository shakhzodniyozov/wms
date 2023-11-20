using Microsoft.EntityFrameworkCore;

namespace WMS.Application;

public interface IApplicationDbContext
{
    DbSet<T> Set<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}