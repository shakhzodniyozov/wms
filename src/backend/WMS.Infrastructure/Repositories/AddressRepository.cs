using Microsoft.EntityFrameworkCore;
using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(IApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<Address?> GetByIdIncludingProductsAsync(Guid id, bool disableTracking = false)
    {
        IQueryable<Address> query = dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        return await query.Include(x => x.ProductAddresses).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
    }
}
