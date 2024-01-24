using WMS.Application;

namespace WMS.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext dbContext;

    public UnitOfWork(IApplicationDbContext context)
    {
        dbContext = context;
    }

    private ICategoryRepository? categoryRepository;
    private IManufacturerRepository? manufacturerRepository;
    private IModelRepository? modelRepository;
    private IProductRepository? productRepository;
    private IAddressRepository? addressRepository;

    public IModelRepository ModelRepository
    {
        get
        {
            modelRepository ??= new ModelRepository(dbContext);
            return modelRepository;
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            categoryRepository ??= new CategoryRepository(dbContext);
            return categoryRepository;
        }
    }

    public IManufacturerRepository ManufacturerRepository
    {
        get
        {
            manufacturerRepository ??= new ManufacturerRepository(dbContext);
            return manufacturerRepository;
        }
    }

    public IProductRepository ProductRepository
    {
        get
        {
            productRepository ??= new ProductRepository(dbContext);
            return productRepository;
        }
    }

    public IAddressRepository AddressRepository
    {
        get
        {
            addressRepository ??= new AddressRepository(dbContext);
            return addressRepository;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
