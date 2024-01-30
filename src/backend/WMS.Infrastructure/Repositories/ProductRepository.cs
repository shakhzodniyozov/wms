using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Product?> GetByEAN(string ean, bool disableTracking = false)
    {
        IQueryable<Product> query = dbSet;

        if (disableTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.EAN == ean);
    }

    public async Task<List<Product>> GetProductsIncludingModels(Expression<Func<Product, bool>> expression = null!, bool disableTracking = false)
    {
        IQueryable<Product> query = dbSet;

        if (disableTracking)
            query = query.AsNoTracking();
        if (expression != null)
            query = query.Where(expression);

        return await query.Include(x => x.Models)
                            .ThenInclude(x => x.Manufacturer)
                              .ThenInclude(x => x.Models)
                          .ToListAsync();
    }
}
