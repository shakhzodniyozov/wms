namespace WMS.Domain;

public class Price : IEntity
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public DateTime DateTime { get; set; }
    public Product Product { get; set; } = null!;
    public Guid ProductId { get; set; }
}
