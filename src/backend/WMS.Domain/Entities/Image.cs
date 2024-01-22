namespace WMS.Domain;

public class Image : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Product? Product { get; set; }
    public Guid ProductId { get; set; }
}
