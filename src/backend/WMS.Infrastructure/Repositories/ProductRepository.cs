using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
