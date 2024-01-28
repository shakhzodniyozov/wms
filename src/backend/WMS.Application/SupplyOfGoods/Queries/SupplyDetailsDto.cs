namespace WMS.Application;

public class SupplyDetailsDto
{
    public Guid ProductId { get; set; }
    public string Product { get; set; } = null!;
    public int Quantity { get; set; }
}
