using Microsoft.EntityFrameworkCore;
using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class SupplyRepository : GenericRepository<SupplyOfGoods>, ISupplyRepository
{
    public SupplyRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SupplyOfGoods?> GetSupplyIncludingProducts(Guid id, bool disableTracking = false)
    {
        IQueryable<SupplyOfGoods> query = dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        return await query.Include(x => x.SupplyOfGoodsDetails)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Models)
                            .ThenInclude(x => x.Manufacturer)
                            .ThenInclude(x => x.Models)
                          .FirstOrDefaultAsync(x => x.Id == id);
    }
}
