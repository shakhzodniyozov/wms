namespace WMS.Application;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    IManufacturerRepository ManufacturerRepository { get; }
    IModelRepository ModelRepository { get; }
    IProductRepository ProductRepository { get; }
    IAddressRepository AddressRepository { get; }
    ISupplyRepository SupplyRepository { get; }
    IEngineRepository EngineRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
