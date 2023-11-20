namespace WMS.Application;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
