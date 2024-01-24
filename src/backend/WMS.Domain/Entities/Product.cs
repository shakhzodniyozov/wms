namespace WMS.Domain;

public class Product : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string? Code { get; set; }
    public decimal LastPrice => Prices.Count == 0 ? 0 : Prices.OrderByDescending(x => x.DateTime).First().Value;
    public decimal CostPrice { get; set; }
    public int Quantity { get; set; }
    public string EAN { get; set; } = null!;
    public string? Description { get; set; }
    public Category Category { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public Image? Image { get; set; }
    public List<Price> Prices { get; set; } = new();
    public List<Model> Models { get; set; } = new();
    public List<ProductAddress> ProductAddresses { get; set; } = new();
    public List<SupplyOfGoodsDetails> SupplyOfGoodsDetails { get; set; } = new();
}