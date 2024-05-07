namespace Domain;

public class SupplyOfGoods : IEntity
{
    public string Id { get; set; } = null!;
    public DateTime Date { get; set; }
    public List<SupplyOfGoodsDetail> Products { get; set; } = [];
}
