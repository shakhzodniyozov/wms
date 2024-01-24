using WMS.Domain;

namespace WMS.Application;

public interface IAddressRepository : IRepository<Address>
{
    Task<Address?> GetByIdIncludingProductsAsync(Guid id, bool disableTracking = false);
}
