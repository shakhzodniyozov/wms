namespace WMS.Application;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Code { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }
}
