
namespace WMS.Domain;

public class SupplyOfGoods : IEntity
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public List<SupplyOfGoodsDetails> SupplyOfGoodsDetails { get; set; } = new();
}
