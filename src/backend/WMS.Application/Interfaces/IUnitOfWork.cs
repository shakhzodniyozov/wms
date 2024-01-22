namespace WMS.Application;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    IManufacturerRepository ManufacturerRepository { get; }
    IModelRepository ModelRepository { get; }
    IProductRepository ProductRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
