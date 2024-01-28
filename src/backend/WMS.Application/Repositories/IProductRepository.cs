using System.Linq.Expressions;
using WMS.Domain;

namespace WMS.Application;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetProductsIncludingModels(Expression<Func<Product, bool>> expression = null!, bool disableTracking = false);
}
