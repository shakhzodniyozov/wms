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
    public ICategoryRepository CategoryRepository
    {
        get
        {
            categoryRepository ??= new CategoryRepository(dbContext);

            return categoryRepository;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
