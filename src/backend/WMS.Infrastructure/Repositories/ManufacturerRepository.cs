using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class ManufacturerRepository : GenericRepository<Manufacturer>, IManufacturerRepository
{
    public ManufacturerRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
