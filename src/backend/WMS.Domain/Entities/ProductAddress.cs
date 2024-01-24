namespace WMS.Domain;

public class ProductAddress : IEntity
{
    public Guid Id { get; set; }
    public Product Product { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Address Address { get; set; } = null!;
    public Guid AddressId { get; set; }
    public int Quantity { get; set; }
}
