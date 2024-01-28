using WMS.Domain;

namespace WMS.Application;

public interface ISupplyRepository : IRepository<SupplyOfGoods>
{
    Task<SupplyOfGoods?> GetSupplyIncludingProducts(Guid id, bool disableTracking = false);
}