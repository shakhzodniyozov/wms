using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class EngineRepository : GenericRepository<Engine>, IEngineRepository
{
    public EngineRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
