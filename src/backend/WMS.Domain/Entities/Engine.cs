
namespace WMS.Domain;

public class Engine : IEntity
{
    public Guid Id { get; set; }
    public double Capacity { get; set; }
    public FuelType FuelType { get; set; }
    public List<Product> Products = new();
}
