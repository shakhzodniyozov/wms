using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IApplicationDbContext dbContext)
        : base(dbContext)
    {

    }
}