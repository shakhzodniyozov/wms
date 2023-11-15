namespace WMS.Domain;

public class Model
{
    public string? ModelName { get; set; }
    public int YearOfIssue { get; set; }
    public BodyTypes BodyType { get; set; }
    public Manufacturer Manufactorer { get; set; } = null!;
    public int ManufactorId { get; set; }
    public List<Product> Products { get; set; } = new();
}