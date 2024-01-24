
namespace WMS.Domain;

public class Address : IEntity
{
    public Guid Id { get; set; }
    public int Line { get; set; }
    public int Section { get; set; }
    public int Level { get; set; }
    public int Cell { get; set; }
    public bool IsTopLevel { get; set; } = false;
    public List<ProductAddress> ProductAddresses { get; set; } = new();
}
