using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class ModelRepository : GenericRepository<Model>, IModelRepository
{
    public ModelRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
