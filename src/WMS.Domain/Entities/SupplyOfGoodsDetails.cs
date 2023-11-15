namespace WMS.Domain;

public class SupplyOfGoodsDetails : IEntity
{
    public Guid Id { get; set; }
    public SupplyOfGoods SupplyOfGoods { get; set; } = null!;
    public Guid SupplyOfGoodsId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
