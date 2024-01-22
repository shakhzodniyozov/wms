namespace WMS.Domain;

public class Model : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int YearOfIssue { get; set; }
    public BodyTypes BodyType { get; set; }
    public Manufacturer Manufacturer { get; set; } = null!;
    public Guid ManufacturerId { get; set; }
    public List<Product> Products { get; set; } = new();
}